using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Proxy
{
    class ViewAction
    {

		/// <summary>
		/// display proxy list
		/// </summary>
        public void DisplayProxyList()
        {
			try
			{
				var proxies = LooperQueries.GetProxies();
				var counter = 0;
				foreach(var proxy in proxies)
				{
					counter++;
					CMD.ShowApplicationMessageToUser($"{counter})\t{proxy}");
				}
			}
			catch (Exception e)
			{
				CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
			}
		}

    }
}
