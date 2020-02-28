using System;
using System.Net;
namespace RecordExecuter.Response {
    public class _Manager {
        public HttpWebResponse _CreateNewResponse (HttpWebRequest request) {
            return (HttpWebResponse) request.GetResponse ();
        }
    }
}