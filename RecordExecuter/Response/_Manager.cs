using System;
using System.Net;
namespace RecordExecuter.Response {
    public class _Manager {
        public HttpWebResponse _CreateNewResponse (HttpWebRequest request) {
            System.Console.WriteLine ($"total proxy:{Storage.ProxyList.Count}");
            var proxy_adr = request.Proxy.GetProxy (request.RequestUri);
            var proxy = $"{proxy_adr.Host}:{proxy_adr.Port}";
            try {
                var response = (HttpWebResponse) request.GetResponse ();
                Storage.Success++;
                System.Console.WriteLine ($"success:{Storage.Success}:{proxy}");
                Storage.GoodProxy.Add (proxy);
                System.Console.WriteLine ($"total good proxy:{Storage.GoodProxy.Count}");
                var page=new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                if(page.ToLower().Contains("welcome")){
                    Storage.GoodCombo++;
                }
                System.Console.WriteLine ($"total good combo:{Storage.GoodCombo}");
                System.Console.WriteLine("--------------------------------------");
                return response;
            } catch (System.Exception) {
                Storage.Bad++;
                System.Console.WriteLine ($"bad:{Storage.Bad}:{proxy}");
                return null;
            }
        }
    }
}