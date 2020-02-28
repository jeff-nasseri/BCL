using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BCL.Request
{
    [ClassCommandInfo("req_view")]
    class _View : ViewAction
    {

        [ClassCommandInfo("prop", "show default properties of request", argsInfo: new string[] { "key=>key of request", "targets=>targets show yellow in console","all=>true for" +
            "display all request propeties" })]
        public void _ShowRequestProperties(string targets = "", string all = "false", string key = null)
        {
            ShowRequestProperties(key, targets, all);
        }


        [ClassCommandInfo("list", "show all requests")]
        public void _ShowRequestList()
        {
            ShowRequestList();
        }


        [ClassCommandInfo("cookie", "show request cookies", argsInfo: new string[] { "url=>for get cookie from request", "key=>request key" })]
        public void _ShowRequestCookieContainer(string url, string key = null)
        {
            ShowRequestCookieContainer(url, key);
        }


        [ClassCommandInfo("current", "show currrent request")]
        public void _ShowCurrentRequest()
        {
            ShowCurrentRequest();
        }

        [ClassCommandInfo("proxy","display request proxy")]
        public void _DisplayRequestProxy(string key = null)
        {
            DisplayRequestProxy(key);
        }

    }
}
