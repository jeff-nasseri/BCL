using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Response
{
    [ClassCommandInfo("res_manager")]
    class _Manager : ManagerAction
    {
        [ClassCommandInfo("get", "get response from request", argsInfo: new string[] { "key=>key of http web request" })]
        public void _CreateNewResponse(string key)
        {
            CreateNewResponse(key);
            //additional command for save auto response stream
            SaveReponseStream(key);
        }
        [ClassCommandInfo("active", "active response", argsInfo: new string[] { "key=>key of http web request" })]
        public void _ActivateResponse(string key)
        {
            ActivateResponse(key);
        }
    }
}
