using System;
using System.Collections.Generic;
using System.Text;

namespace BCL
{
    class VariableAnalysis
    {

        //Commad kind : Add
        static string Kind { get; set; }

        /// <summary>
        /// Processing commands related to variables
        /// </summary>
        /// <param name="command">Executeable command</param>
        /// <param name="value">Optional paramater for add new variable</param>
        public static void ExecuteVariableCommand(string command, string value = "")
        {
            try
            {
                var array = Utilities.GetArray(command, Utilities.Mode_3);
                Kind = array[0];
                switch (Kind.ToLower())
                {
                    case "add":
                        VariablesStorageQueries.AddNewVariable(array[1], value); CMD.ShowApplicationMessageToUser($"{value} was added to var list", showType: ShowType.SUCCESS); break;
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : BLC.VariableAnalysis", showType: ShowType.DANGER);
            }
        }

        //TODO : complete this function
        public static string ExecuteVariableCommand(string command)
        {
            try
            {
                var array = command.Split(Utilities.Mode_3);
                var r = array[0];
                if (array[0] == "var")
                {
                    //Execute data
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : BLC.VariableAnalysis", showType: ShowType.DANGER);
            }
            return null;
        }
    }
}
