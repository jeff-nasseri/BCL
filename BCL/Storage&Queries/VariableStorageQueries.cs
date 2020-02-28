using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BCL
{
    public class VariablesStorageQueries : VariablesStorage
    {
        /// <summary>
        /// Return list of variables
        /// </summary>
        public static List<(string name, object value)> GetVariables()
        {
            return VariablesStorage.VariablesList;
        }

        /// <summary>
        /// Add new variables to variables list by name and value
        /// </summary>
        /// <param name="name">Name of varible</param>
        /// <param name="value">Value of variable</param>
        public static void AddNewVariable(string name, object value)
        {

            if (!IsExistVariable(name))
                VariablesList.Add((name, value));
            else
                throw new Exception("variables with same name exist!");
        }

        /// <summary>
        /// Check the variables list for particular variable by name
        /// </summary>
        /// <param name="name">Name of variable</param>
        public static bool IsExistVariable(string name)
        {
            return VariablesList.Any(var => var.name == name);
        }

        /// <summary>
        /// Remove variable from variables list by variable name
        /// </summary>
        /// <param name="name">Name of variable</param>
        public static void RemoveVariable(string name)
        {
            if (IsExistVariable(name))
                VariablesList.Remove((name, VariablesList.Single(var => var.name == name).value));
            else
                throw new Exception("No variables with this name exist");
        }

        /// <summary>
        /// Remove all variables from variables list
        /// </summary>
        public static void RemoveAllVariables()
        {
            VariablesList.Clear();
        }

        /// <summary>
        /// Get value from particular variable
        /// </summary>
        /// <param name="name">Name of variable</param>
        public static object GetVariableValue(string name)
        {
            if (IsExistVariable(name))
                return VariablesList.Single(var => var.name == name).value;
            else
                throw new Exception("No variables with this name exist");
        }

    }
}
