using System;
using System.Collections.Generic;
using System.Text;

namespace BCL
{
    [AttributeUsage(AttributeTargets.All)]
    public class ClassCommandInfo : Attribute
    {
        public string CommandName { get; set; }
        public string CommandDescription { get; set; }
        public string OnSuccessMessage { get; set; }

        public List<(string name, string description)> ArgsInfo = new List<(string name, string description)>();

        public ClassCommandInfo(string commandName, string commandDescription = "", string onSuccessMessage = "", string[] argsInfo = null)
        {
            CommandName = commandName;
            CommandDescription = commandDescription;
            OnSuccessMessage = onSuccessMessage;
            if (argsInfo != null)
            {
                SetArgumantsInfo(argsInfo);
            }
        }
        void SetArgumantsInfo(string[] argsInfo)
        {
            for (int i = 0; i < argsInfo.Length; i++)
            {
                var name = argsInfo[i].Split(Utilities.Mode_5)[0];
                var description = argsInfo[i].Split(Utilities.Mode_5)[1];
                ArgsInfo.Add((name, description));
            }
        }
    }
}
