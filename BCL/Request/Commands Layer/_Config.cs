using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace BCL.Request
{
    [ClassCommandInfo("req_config")]
    class _Config : ConfigAction
    {

        [ClassCommandInfo("add_header", "add new header(s) to request", argsInfo: new string[] { "headers=>(Method->GET,Accept->*) or (Accept->var::value)" })]
        public void _AddHeaderToRequest(string name,string value, string key = null)
        {
            AddHeaderToRequest(key, name,value);
        }


        [ClassCommandInfo("add_header_custome", "add special header with key and value .", argsInfo: new string[] { "name=>header name", "value=>heaader value",
            "key=>key of request" })]
        public void _AddCustomeHeaderToRequest(string name, string value, string key = null)
        {
            AddCustomeHeaderToRequest(key, name, value);
        }


        [ClassCommandInfo("add_cookie", "add new cookie to request", argsInfo: new string[] { "name=>cookie name", "value=>cookie value",
            "path=>cookie path","domain=>specify domain for store cookie","key=>request key"})]
        public void _AddNewCookie(string name, string value, string path, string domain, string key = null)
        {
            AddNewCookie(name, value, path, domain, key);
        }


        [ClassCommandInfo("remove_cookie", "remove cookie from cookie container", argsInfo: new string[] { "key=>request key", "url=>cookie domain (url format)" })]
        public void _RemoveCookie(string key, string url)
        {
            RemoveCookie(key, url);
        }


        [ClassCommandInfo("write", "write data on request", argsInfo: new string[] { "data=>user input data such as username and password or (var::name)", "key=>key of request" })]
        public void _WriteData(string data, string key = null)
        {
            WriteData(key, data);
        }


        [ClassCommandInfo("useragent", "set useragent", argsInfo: new string[] {
            "agent=>" +
            "1 : Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36 OPR/65.0.3467.78"})]
        public void _SetUserAgent(string agent, string key = null)
        {
            SetUserAgent(key, agent);
        }


        [ClassCommandInfo("copy_config", "copy request headers", argsInfo: new string[] { "name=>variable name", "targets=>target(s) for copy in variables list", "key=>request key" })]
        public void _CopyRequestConfig(string name, string targets = null, string key = null)
        {
            CopyRequestConfig(name, targets, key);
        }


        [ClassCommandInfo("req_remove", "remove request", argsInfo: new string[] { "key=>request key" })]
        public void _RemoveRequest(string key = null)
        {
            RemoveRequest(key);
        }


        [ClassCommandInfo("paste_config", "paste config to request", argsInfo: new string[] { "varName=>variable name for get value pair from variable list", "key=>request key" })]
        public void _PasteConfig(string varName, string key = null)
        {
            PasteConfig(varName, key);
        }


        [ClassCommandInfo("set_proxy")]
        public void _SetProxyToRequest(string proxy, string key = null)
        {
            SetProxyToRequest(proxy, key);
        }

    }
}
