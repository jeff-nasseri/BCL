using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BCL.Response
{
    class FetchAction
    {
        
        HtmlDocument document = new HtmlDocument();
        HtmlDocument GetDocument(Stream stream)
        {
            document.Load(stream);
            stream.Position = 0;
            return document;
        }


        /// <summary>
        /// fetch data and value from web page
        /// </summary>
        /// <param name="key">response</param>
        /// <param name="tag">Intended html element name</param>
        /// <param name="attribute">Intended html attribute</param>
        /// <param name="target">Intended target (attribute name)</param>
        /// <param name="varCommand">The command to be processed to store the value</param>
        protected void FetchValueFromHtmlPage(string key, string tag, string attribute, string target, string varCommand)
        {
            try
            {
                var stream = ProgramStorageQueries.GetResponseStream(key);
                var data = GetDocument(stream);
                var html = data.DocumentNode.SelectSingleNode($"//{tag}[@{attribute}]");
                var result = html.Attributes[target].Value;
                VariableAnalysis.ExecuteVariableCommand(varCommand, result);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// fetch header value from response headers
        /// </summary>
        /// <param name="name">header name</param>
        /// <param name="key">response key</param>
        /// <param name="varCommand">The command to be processed to store the value</param>
        protected void FetchHeader(string name, string key, string varCommand)
        {
            try
            {
                var response = ProgramStorageQueries.GetResponse(key);
                foreach (var item in response.Headers)
                {
                    if (item.ToString() == name)
                    {
                        var val = response.Headers.GetValues(item.ToString())[0];
                        VariableAnalysis.ExecuteVariableCommand(varCommand, val);
                    }
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

    }
}
