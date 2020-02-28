using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Proxy
{
    [ClassCommandInfo("proxy_view")]
    class _View:ViewAction
    {
        [ClassCommandInfo("list","display list of proxies")]
        public void _DisplayProxyList()
        {
            DisplayProxyList();
        }


    }
}
