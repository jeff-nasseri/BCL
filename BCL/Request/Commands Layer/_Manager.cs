using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;

namespace BCL.Request
{
    [ClassCommandInfo("req_manager")]
    class _Manager : ManagerAction
    {

        [ClassCommandInfo("generate", "generate new request",argsInfo:new string[] { "key=>create request and marked with key", "url=>request url" })]
        public void _GenerateNewRequest(string key, string url)
        {
            GenerateNewRequest(key, url);
        }
        [ClassCommandInfo("active", "set this request to current request",argsInfo:new string[] { "key=>key of request" })]
        public void _ActivateRequest(string key)
        {
            ActivateRequest(key);
        }
    }
}
