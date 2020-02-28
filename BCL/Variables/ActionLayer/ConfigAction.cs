using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Variables
{
    class ConfigAction
    {
        //additional command :

        //public void PluseMultiVariables(string command)
        //{
        //    VariableAnalysis.ExecuteVariableCommand(command: command);
        //}

        //protected void CutVariableAndSave(string name, string newName, string startStr, string endStr, string startNum, string endNum)
        //{
        //    VariableStorageQueries.AddNewVariable(newName, Utilities.GetSubstring(VariableStorageQueries.GetVariableValue(name), startStr, endStr, startNum, endNum));
        //}

        //protected void ReplaceDataInVariable(string newValue, string oldValue, string name)
        //{
        //    var variable = variablestoragequeries.getvariable(name);
        //    var value = variable.value.replace(oldvalue, variableanalysis.executevariablecommand(newvalue) ?? newvalue);
        //    variablestoragequeries.updatevariable(name, value);
        //}


        /// <summary>
        /// Adds new varible in variables list
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="value">Value of variable</param>
        public void AddNewVariable(string name, string value)
        {
            VariablesStorageQueries.AddNewVariable(name, value);
        }

        /// <summary>
        /// Remove variable from variabes list
        /// </summary>
        /// <param name="variables">Variable names</param>
        public void RemoveVariable(string variables)
        {
            try
            {
                var array = Utilities.GetArray(variables, Utilities.Mode_1);
                for (int i = 0; i < array.Length; i++)
                {
                    VariablesStorageQueries.RemoveVariable(array[i]);
                    CMD.ShowApplicationMessageToUser($"var {array[i]} removed", showType: ShowType.SUCCESS);
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// Clear variables list
        /// </summary>
        public void RemoveAllVariables()
        {
            VariablesStorageQueries.RemoveAllVariables();
        }

    }
}
