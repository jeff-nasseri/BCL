using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.Json.Serialization;

namespace BCL
{
    public class ProgramStorage
    {

        /// <summary>
        /// List of web requests
        /// </summary>
        protected static IDictionary<string, HttpWebRequest> RequestDictionary = new Dictionary<string, HttpWebRequest>();

        /// <summary>
        /// List of web response
        /// </summary>
        protected static IDictionary<string, HttpWebResponse> ResponsetDictionary = new Dictionary<string, HttpWebResponse>();

        /// <summary>
        /// Information list of classes that are run as commands (feature name,info=>(name,value))
        /// </summary>
        protected static IList<(ClassesCommandFeaturesName, (string name, object value))> ClassCommandInfo = new List<(ClassesCommandFeaturesName, (string, object))>();

        /// <summary>
        /// List of stream for responses
        /// </summary>
        protected static IList<(string key, Stream stream)> ResponsesStream = new List<(string key, Stream stream)>();

        /// <summary>
        /// List of stream for requests
        /// </summary>
        protected static IList<(string key, Stream stream)> RequestStreams = new List<(string key, Stream stream)>();

        /// <summary>
        /// List of cookie container
        /// </summary>
        protected static IList<(string key, CookieContainer container)> CookieContainerList = new List<(string key, CookieContainer container)>();

        /// <summary>Newtonsoft.Json.JsonSerializationException: 'Error getting value from 'Cookies' on 'System.Net.HttpWebResponse'.'

        /// List of command for functions
        /// </summary>
        protected static IEnumerable<string[]> FunctionsCommand { get; set; }

        /// <summary>
        /// Current enabled request
        /// </summary>
        protected static HttpWebRequest CurrentRequest { get; set; }

        /// <summary>
        /// Current enabled response
        /// </summary>
        protected static HttpWebResponse CurrentResponse { get; set; }


    }
}
