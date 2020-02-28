using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Variables
{
    [ClassCommandInfo("var_config")]
    class _Config : ConfigAction
    {
        //additional commend :

        //[CommandInfo("pluse", "pluse variable in variable list  FORMAT : ", argsInfo: new string[] { "command=>executeable variable command   SAMPLE (pluse:name1,name2:name)" })]
        //public void _PluseMultiVariables(string command)
        //{
        //    PluseMultiVariables(command);
        //}        

        //[CommandInfo("cut", "cut variable and save them", argsInfo: new string[] { "name=>variable name", "newName=>new name for variable", "startStr=>started string", "endStr=>ended string",
        //"startNum=>Changes for start number","endNum=>Changes for end number"})]
        //public void _CutVariableAndSave(string name, string newName, string startStr, string endStr, string startNum = "0", string endNum = "0")
        //{
        //    CutVariableAndSave(name, newName, startStr, endStr,startNum,endNum);
        //}

        //[CommandInfo("replace", "replace data in variable",argsInfo:new string[] { "newValue=>value for putting up","oldValue=>replacement value"})]
        //public void _ReplaceDataInVariable(string newValue,string oldValue, string name)
        //{
        //    ReplaceDataInVariable(newValue, oldValue, name);
        //}


        [ClassCommandInfo("add", "add new variable", argsInfo: new string[] { "name=>variable name", "value=>variable value" })]
        public void _AddNewVariable(string name, string value)
        {
            AddNewVariable(name, value);
        }
      
        
        [ClassCommandInfo("remove", "remove variables", argsInfo: new string[] { "names=>remove variables with name" })]
        public void _RemoveVariable(string names)
        {
            RemoveVariable(names);
        }
        
        
        [ClassCommandInfo("remove_all", "remove all variables")]
        public void _RemoveAllVariables()
        {
            RemoveAllVariables();
        }

    }
}
