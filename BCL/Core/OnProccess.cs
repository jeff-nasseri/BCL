using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BCL
{
    class OnProccess
    {
        /// <summary>
        /// All classes type that are run by command
        /// </summary>
        readonly IEnumerable<Type> CommandsTypesClass;
        public OnProccess(IEnumerable<Type> types)
        {
            CommandsTypesClass = types;
        }
        /// <summary>
        /// Taking the command line application instance,route command,route name from
        /// relevant class and putting it in the program memory
        /// </summary>
        public void SetAppsInStaticStorage()
        {
            foreach (var type in CommandsTypesClass)
            {
                var app = (Activator.CreateInstance(typeof(CommandLineApplication<>).MakeGenericType(type), new object[] { true })
                    as CommandLineApplication);

                ProgramStorageQueries.AddNewClassCommandInfo((ClassesCommandFeaturesName.AppsList, (type.ToString(), app)));

                var attr = type.GetCustomAttribute(typeof(ClassCommandInfo));
                ProgramStorageQueries.AddNewClassCommandInfo((ClassesCommandFeaturesName.ClassesCommand, (type.ToString(),
                    (attr as ClassCommandInfo).CommandName)));

                ProgramStorageQueries.AddNewClassCommandInfo((ClassesCommandFeaturesName.ClassesName, (type.ToString(), type.ToString())));
            }
        }
        public void InsertFunctionsCommandInStaticStorage()
        {
            var commands = CommandsTypesClass.Select(cls => cls.GetMethods())
    .Select(m => m.Where(m => m.Name.StartsWith(Utilities.Mode_4)).Select(m => (m.GetCustomAttribute(typeof(ClassCommandInfo)) as ClassCommandInfo).CommandName))
    .Select(name => name.ToArray());
            ProgramStorageQueries.SetFunctionsCommand(commands);

        }
        public void CallConventions()
        {
            var apps = ProgramStorageQueries.GetFeatureInfo(ClassesCommandFeaturesName.AppsList).Select(item => item.Item2);
            foreach (var app in apps)
            {
                (app as CommandLineApplication).HelpOption(inherited: true);
                (app as CommandLineApplication).Conventions
                    .AddConvention(new MethodsAsSubcommandsConventions())
                    .SetAppNameFromEntryAssembly();
            }
        }
    }

}
