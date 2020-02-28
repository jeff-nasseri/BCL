using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Proxy
{
    [ClassCommandInfo("proxy_config")]
    class _Config : ConfigAction
    {
    
        [ClassCommandInfo("set", "set proxy list from file", argsInfo: new string[] { "path=>file path for load proxies" })]
        public void _SetProxies(string path)
        {
            SetProxies(path);
        }

        [ClassCommandInfo("write", "write proxies in proxy file")]
        public void _WriteProxies(string path)
        {
            WriteProxies(path);
        }

        [ClassCommandInfo("remove", "remove proxy from proxy list")]
        public void _RemoveProxy(string proxy)
        {
            RemoveProxy(proxy);
        }

        [ClassCommandInfo("check", "check connection for prxies and remove bad proxies")]
        public void _CheckProxyConnection(string thread = "20", string address = "http://google.com",string timeout="10000")
        {
            CheckProxyConnection(thread,address,timeout);
        }
    }
}
