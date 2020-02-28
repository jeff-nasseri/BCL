using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace RecordExecuter.Response {
    public class _Fetch {

        HtmlDocument GetDocument (Stream stream) {
            HtmlDocument document = new HtmlDocument ();
            document.Load (stream);
            return document;
        }
        public string _FetchValueFromHtmlPage (HttpWebResponse response, string tag, string attribute, string target) {
            Stream stream = response.GetResponseStream ();
            var data = GetDocument (stream);
            var html = data.DocumentNode.SelectSingleNode ($"//{tag}[@{attribute}]");
            var result = html.Attributes[target].Value;
            stream.Close ();
            return result;
        }
    }
}