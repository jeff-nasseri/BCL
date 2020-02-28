using System;
using System.Collections.Generic;
using System.Net;
namespace RecordExecuter.Request {
    public class _Manager {
        public HttpWebRequest _GenerateNewRequest (string url) {
            var request = (HttpWebRequest) WebRequest.Create (url);
            request.CookieContainer = new CookieContainer ();
            return request;
        }
    }
}