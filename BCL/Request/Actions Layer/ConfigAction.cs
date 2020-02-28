using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace BCL.Request {
    public class ConfigAction {

        /// <summary>
        /// Add http header to request
        /// </summary>
        /// <param name="key">request key</param>
        /// <param name="headers">http web headers</param>
        protected void AddHeaderToRequest (string key, string name, string value) {
            try {
                var request = ProgramStorageQueries.GetRequest (key);
                var prop = request.GetType ().GetProperty (name);
                prop.SetValue (request, VariableAnalysis.ExecuteVariableCommand (command: value) ?? value);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// add new http header to request
        /// </summary>
        /// <param name="key">request key</param>
        /// <param name="name">header name</param>
        /// <param name="value">header value</param>
        protected void AddCustomeHeaderToRequest (string key, string name, string value) {
            try {
                var request = ProgramStorageQueries.GetRequest (key);
                request.Headers.Add (name, VariableAnalysis.ExecuteVariableCommand (value) ?? value);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// add cookie to request cookie container
        /// </summary>
        /// <param name="Key">request key</param>
        /// <param name="name">cookie name</param>
        /// <param name="value">cookie value</param>
        /// <param name="path">cookie save path in server</param>
        /// <param name="domain">cookie domain</param>
        protected void AddNewCookie (string name, string value, string path, string domain, string Key = null) {
            try {
                var request = ProgramStorageQueries.GetRequest (Key);
                var cookie = new Cookie (name, VariableAnalysis.ExecuteVariableCommand (value) ?? value, path, domain);
                var cookieContainer = ProgramStorageQueries.GetCookieContainer (ProgramStorageQueries.GetRequestKey (request));
                cookieContainer.Add (cookie);
                request.CookieContainer = cookieContainer;
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// remove cookie from request cookie container
        /// </summary>
        /// <param name="key">request key</param>
        /// <param name="url">request url</param>
        protected void RemoveCookie (string key, string url) {
            try {
                foreach (Cookie cookie in ProgramStorageQueries.GetCookieContainer (key).GetCookies (new Uri (url))) {
                    cookie.Expires = DateTime.Now.Subtract (TimeSpan.FromDays (1));
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// write data on request stream
        /// </summary>
        /// <param name="key">request key</param>
        /// <param name="data">data for write</param>
        protected void WriteData (string key, string data) {
            try {
                var request = ProgramStorageQueries.GetRequest (key);
                var stream = request.GetRequestStream ();
                byte[] bytes = Encoding.ASCII.GetBytes (VariableAnalysis.ExecuteVariableCommand (data) ?? data);
                request.ContentLength = bytes.Length;
                stream.Write (bytes, 0, bytes.Length);
                stream.Close ();
                CMD.ShowApplicationMessageToUser ($"{VariableAnalysis.ExecuteVariableCommand(data) ?? data} wited on request\nlentgh : {bytes.Length}  content_legth set auto", showType : ShowType.SUCCESS);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// set useragent on request
        /// </summary>
        /// <param name="key">request key</param>
        /// <param name="agent">
        /// number for agent :
        /// 1 : Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36 OPR/65.0.3467.78
        /// </param>
        protected void SetUserAgent (string key, string agent) {
            try {
                var request = ProgramStorageQueries.GetRequest (key);
                switch (agent) {
                    case "1":
                        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36 OPR/65.0.3467.78";
                        break;
                    default:
                        throw new Exception ("agent number not valid");
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// copy all headers of request in variables list
        /// </summary>
        /// <param name="variableName">The name of the variable that holds the config</param>
        /// <param name="targets">defined header</param>
        /// <param name="key">request key</param>
        protected void CopyRequestConfig (string variableName, string targets = null, string key = null) {
            try {
                var request = ProgramStorageQueries.GetCurrentRequest ();
                if (targets == null) {
                    var properties = CopyConfig (request);
                    VariablesStorageQueries.AddNewVariable (variableName, properties);
                } else {
                    var properties = CopyConfig (request, targets);
                    VariablesStorageQueries.AddNewVariable (variableName, properties);
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// copy request headers by default specific headers
        /// </summary>
        /// <param name="request">http reuest</param>
        /// <returns>enumerable of anonymouse type</returns>
        private object CopyConfig (HttpWebRequest request) {
            return request.GetType ().GetProperties ().Where (prop => Utilities.DefaultRequestShowableHeaders.Any (str => str == prop.Name)).
            Select (prop => new {
                Name = prop.Name,
                    Value = prop.GetValue (request)
            });
        }

        /// <summary>
        /// copy request headers by targets
        /// </summary>
        /// <param name="request">http request</param>
        /// <param name="targets">htt headers for copy</param>
        /// <returns>enumerable of anonymouse type</returns>
        private object CopyConfig (HttpWebRequest request, string targets) {
            var array = Utilities.GetArray (targets, Utilities.Mode_1);
            return request.GetType ().GetProperties ().Where (prop => array.Any (item => item == prop.Name)).
            Select (prop =>
                new {
                    Name = prop.Name,
                        Value = prop.GetValue (request)
                });
        }

        /// <summary>
        /// remove request from 
        /// </summary>
        /// <param name="key">request key</param>
        protected void RemoveRequest (string key = null) {
            try {
                if (key == null)
                    ProgramStorageQueries.RemoveCurrentRequest ();
                else
                    ProgramStorageQueries.RemoveRequest (key);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// paste config to request specified
        /// </summary>
        /// <param name="varName">The variable in which the configuration is stored</param>
        /// <param name="key">request key</param>
        protected void PasteConfig (string varName, string key = null) {

            try {
                var request = ProgramStorageQueries.GetRequest (key);

                var config = VariablesStorageQueries.GetVariableValue (varName) as IEnumerable<object>;

                foreach (dynamic item in config) {
                    var name = item.Name as string;
                    var value = item.Value;
                    var prop = request.GetType ().GetProperty (name);
                    prop.SetValue (request, item.Value);
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }

        }

        /// <summary>
        /// set proxy for request
        /// </summary>
        /// <param name="proxy">web proxy</param>
        /// <param name="key">request key</param>
        public void SetProxyToRequest (string proxy, string key) {
            try {
                var ip = proxy.Split (':') [0];
                var port = Int32.Parse (proxy.Split (':') [1]);
                var request = ProgramStorageQueries.GetRequest (key);
                request.Proxy = new WebProxy (ip, port);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

    }

}