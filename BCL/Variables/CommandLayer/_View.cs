using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Variables
{
    [ClassCommandInfo("var_view")]
    class _View : ViewAction
    {

        [ClassCommandInfo("list", "show variable list")]
        public void _ShowVariableList()
        {
            ShowVariableList();
        }
        
        
        [ClassCommandInfo("for_each", "show all variable properties", argsInfo: new string[] { "name=>variable name" })]
        public void _ShowVariableProperties(string name)
        {
            ShowVariableProperties(name);
        }


    }
}
