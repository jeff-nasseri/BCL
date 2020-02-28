using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Variables
{
    class ViewAction
    {

        /// <summary>
        /// display variable in variables list
        /// </summary>
        public void ShowVariableList()
        {
            var count = 1;
            foreach (var variable in VariablesStorageQueries.GetVariables())
            {
                CMD.ShowApplicationMessageToUser($"{count++} ) {variable.Item1}\t{variable.Item2}");
            }
        }

        /// <summary>
        /// Looping in the list of effects whose value is enumerable
        /// </summary>
        /// <param name="name">Name of variable</param>
        public void ShowVariableProperties(string name)
        {
            var value = VariablesStorageQueries.GetVariableValue(name);
            var count = 0;
            foreach(var item in value as IEnumerable<object>)
            {
                CMD.ShowApplicationMessageToUser($"{count++} )\t{item}");
            }
        }
    }
}
