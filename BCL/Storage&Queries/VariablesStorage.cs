using System;
using System.Collections.Generic;
using System.Text;

namespace BCL
{
    public class model
    {
        public string name { get; set; }
        public object value { get; set; }
    }
    public class VariablesStorage
    {

        /// <summary>
        /// List of variables
        /// </summary>
        protected static List<(string name, object value)> VariablesList = new List<(string name, object value)>();
    }
}
