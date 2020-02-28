using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;


namespace BCL
{
    public class MethodsAsSubcommandsConventions : IConvention
    {
        public void Apply(ConventionContext context)
        {

            if (context.ModelType == null)
            {
                return;
            }
            //BindingFlags.Public | BindingFlags.Instance
            var handleMethods = context.ModelType.GetMethods()
                .Where(m => m.Name.StartsWith(Utilities.Mode_4, StringComparison.Ordinal));

            foreach (var method in handleMethods)
            {


                var methodAttribute = method.GetCustomAttribute(typeof(ClassCommandInfo)) as ClassCommandInfo;
                var subCommandName = methodAttribute == null ? method.Name.ToLower() : methodAttribute.CommandName;
                var subCommmandDescription = methodAttribute == null ? $"description is about {method.Name.ToLower()}" : methodAttribute.CommandDescription;


                context.Application.Command(subCommandName, cmd =>
                {
                    cmd.Description = subCommmandDescription;

                    var parameters = method.GetParameters();

                    var paramValueFactories = new Func<object>[parameters.Length];

                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var param = parameters[i];

                        var paramDescription = methodAttribute.ArgsInfo != null ? methodAttribute.ArgsInfo.SingleOrDefault(arg => arg.name == param.Name.ToLower()).description : null;

                        if (param.HasDefaultValue)
                        {

                            var template = $"--{param.Name.ToLowerInvariant()} || -{param.Name.ToLowerInvariant()[0]}";

                            var opt = cmd.Option(template, paramDescription ?? "", CommandOptionType.SingleValue);

                            paramValueFactories[i] = () =>
                            {
                                if (!opt.HasValue()) return param.DefaultValue;
                                return opt.Value();
                            };

                        }
                        else
                        {

                            var arg = cmd.Argument(param.Name, paramDescription ?? "").IsRequired();

                            paramValueFactories[i] = () => arg.Value;

                        }
                    }
                    cmd.OnExecuteAsync(async cancellationToken =>
                    {
                        var modelInstance = context.ModelAccessor.GetModel();
                        var methodArgs = paramValueFactories.Select(f => f.Invoke()).ToArray();
                        //first add to record and next invoke
                        if(RecordQueries.GetRecordState())
                            RecordQueries.AddNewCommad((modelInstance, method, methodArgs));
                        var value = method.Invoke(modelInstance, methodArgs);
                        if (value is int retVal) return retVal;
                        if (value is Task<int> retTask) return await retTask;
                        if (value is Task retWait) await retWait;
                        return 0;
                    });
                });
            }
        }
    }


}
