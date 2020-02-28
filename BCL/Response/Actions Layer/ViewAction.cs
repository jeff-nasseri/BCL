using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BCL.Response
{
    class ViewAction
    {

        /// <summary>
        /// display response properties
        /// </summary>
        /// <param name="key">response id paire with request id</param>
        /// <param name="targets">The properties that are Included are shown in yellow</param>
        /// <param name="all">If set to true all properties of http web request will be display</param>
        protected void ShowResponsePropeties(string key, string targets, string all)
        {
            try
            {
                var response = ProgramStorageQueries.GetResponse(key);
                var targetsArray = Utilities.GetArray(targets, Utilities.Mode_1);
                var targetProperties = all == "true" ? response.GetType().GetProperties() : (response.GetType().GetProperties().Where(p => Utilities.DefaultResponseShowableHeaders
                   .Any(str => str.Contains(p.Name))));
                var count = 1;
                foreach (var pi in targetProperties)
                {
                    CMD.ShowApplicationMessageToUser(
                        $"{count++} ) {pi.Name} : {pi.GetValue(response, null)}"
                        , showType: targetsArray.Any(str => str.Contains(pi.Name)) && targets != null ?
                        ShowType.DataTarget : ShowType.INFO
                        );
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// display all response in response dictionary
        /// </summary>
        protected void ShowResponseList()
        {
            var count = 1;
            foreach (var response in ProgramStorageQueries.GetResponses())
            {
                CMD.ShowApplicationMessageToUser($"{count++} )key : {response.Key} \tvalue : {response.Value.ResponseUri}");
            }
        }

        /// <summary>
        /// display current response
        /// </summary>
        protected void ShowCurrentResponse()
        {
            try
            {
                var response = ProgramStorageQueries.GetCurrentResponse();
                CMD.ShowApplicationMessageToUser($"{ProgramStorageQueries.GetResponseKey(response ?? throw new Exception("response not valid"))} \t " +
                    $": {ProgramStorageQueries.GetCurrentResponse().ResponseUri}");
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// display web page source from response stream
        /// </summary>
        /// <param name="key">response key</param>
        protected void ShowHtmlPage(string key)
        {
            try
            {
                var stream = ProgramStorageQueries.GetResponseStream(key);
                var htmlPage = new StreamReader(stream).ReadToEnd();
                stream.Position = 0;
                CMD.ShowApplicationMessageToUser($"{htmlPage}");
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// validate web page source for specific target
        /// </summary>
        /// <param name="key">response key</param>
        /// <param name="target">the value to look at</param>
        protected void IsHtmlPageContain(string key, string target)
        {
            try
            {
                var stream = ProgramStorageQueries.GetResponseStream(key);
                string htmlPage = new StreamReader(stream).ReadToEnd();
                stream.Position = 0;
                bool awnser = false;
                var array = Utilities.GetArray(target, Utilities.Mode_1);
                for (int i = 0; i < array.Length; i++)
                {
                    awnser = htmlPage.Contains(array[i]);
                }
                CMD.ShowApplicationMessageToUser($"awnser : {awnser}", showType: awnser ? ShowType.SUCCESS : ShowType.DANGER);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// diaplay response cookies
        /// </summary>
        /// <param name="key">response key</param>
        protected void ShowResponseCookieCollaction(string key)
        {
            try
            {
                var response = ProgramStorageQueries.GetResponse(key);
                var collaction = response.Cookies;
                var count = 0;
                foreach (System.Net.Cookie cookie in collaction)
                {
                    CMD.ShowApplicationMessageToUser($"{count++} ) [clr" +
                        $"name : {cookie.Name}] [value : {cookie.Value}] [path : {cookie.Path}] [domain : {cookie.Domain}]");
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }
    }
}
