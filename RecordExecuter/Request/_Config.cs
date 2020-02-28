using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace RecordExecuter.Request {
    public class _Config {
        public void _AddCustomeHeaderToRequest (HttpWebRequest request, string name, string value) {
            request.Headers.Add (name, value);
        }

        public void _AddHeaderToRequest (HttpWebRequest request, string name, string value) {
            var prop = request.GetType ().GetProperty (name);
            prop.SetValue (request, value);
        }
        public void _AddNewCookie (HttpWebRequest request, string name, string value, string path, string domain) {
            request.CookieContainer.Add (new Cookie (name, value, path, domain));
        }
        public void _SetUserAgent (HttpWebRequest request, string agent) {
            switch (agent) {
                case "1":
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36 OPR/65.0.3467.78";
                    break;
                default:
                    throw new Exception ("agent number not valid");
            }
        }
        public void _SetProxyToRequest (HttpWebRequest request, string proxy) {
            var ip = proxy.Split (':') [0];
            var port = Int32.Parse (proxy.Split (':') [1]);
            request.Proxy = new WebProxy (ip, port);
        }
        public void _WriteData (HttpWebRequest request, string data) {
            Stream stream = request.GetRequestStream ();
            byte[] bytes = Encoding.ASCII.GetBytes (data);
            request.ContentLength = bytes.Length;
            stream.Write (bytes, 0, bytes.Length);
            stream.Close ();
        }

    }
}