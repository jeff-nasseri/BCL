using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BCL.Response
{
    [ClassCommandInfo("res_fetch")]
    class _Fetch : FetchAction
    {


        [ClassCommandInfo("page", "fetch value from html page", argsInfo: new string[] { "key=>key of request","tag=>tag name","attribute=> name and value of one of the target attribute",
        "target=>get value from target attribute","varcommand=>execute variable commands"})]
        public void _FetchValueFromHtmlPage(string tag, string attribute, string target, string varCommand = "", string key = null)
        {
            FetchValueFromHtmlPage(key, tag, attribute, target, varCommand);
        }
        
        
        [ClassCommandInfo("header", "fetch header value", argsInfo: new string[] { "name=>header name", "key=>key of response", "varcommand=>execute variable commands" })]
        public void _FetchHeader(string name, string key = null, string varCommand = "")
        {
            FetchHeader(name, key, varCommand);
        }


    }
}
