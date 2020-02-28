using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCL.Response
{
    [ClassCommandInfo("res_view", "response view .")]
    class _View : ViewAction
    {
        
        [ClassCommandInfo("prop", "show response properties .", argsInfo: new string[] { "key=>key of http web request", 
            "targets=>targets show yellow in console", "all=>if set true show all properties" })]
        public void _ShowResponsePropeties(string key = null, string targets = "", string all = "false")
        {
            ShowResponsePropeties(key, targets, all);
        }


        [ClassCommandInfo("list", "show response list .")]
        public void _ShowResponseList()
        {
            ShowResponseList();
        }
        
        
        [ClassCommandInfo("current", "show current response .")]
        public void _ShowCurrentResponse()
        {
            ShowCurrentResponse();
        }
        
        
        [ClassCommandInfo("page", "show html of page", argsInfo: new string[] { "key=>key of request" })]
        public void _showHtmlPage(string key = null)
        {
            ShowHtmlPage(key);
        }
        
        
        [ClassCommandInfo("is_contain", "check html of page for contain target(s)")]
        public void _IsHtmlPageContain(string target, string key = null)
        {
            IsHtmlPageContain(key, target);
        }
        
        
        [ClassCommandInfo("cookie", "show response cookies", argsInfo: new string[] { "key=>response key" })]
        public void _ShowResponseCookieCollaction(string key = null)
        {
            ShowResponseCookieCollaction(key);
        }

    }
}
