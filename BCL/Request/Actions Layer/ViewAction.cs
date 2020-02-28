using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BCL.Request
{
    public class ViewAction
    {
        /// <summary>
        /// Show properties of request .properties contain http web headers
        /// </summary>
        /// <param name="key">Key of request if leave that null current request returned</param>
        /// <param name="targets">The properties that are Included are shown in yellow</param>
        /// <param name="all">If set to true all properties of http web request will be display</param>
        protected void ShowRequestProperties(string key, string targets, string all)
        {
            var request = ProgramStorageQueries.GetRequest(key);
            try
            {
                var targetsArray = Utilities.GetArray(targets, Utilities.Mode_1);
                var targetProperties = all == "true" ? request.GetType().GetProperties() : request.GetType().GetProperties()
                    .Where(p => Utilities.DefaultRequestShowableHeaders.Any(str => str.Contains(p.Name))).Select(p => p);

                var count = 1;
                foreach (var pi in targetProperties)
                {
                    CMD.ShowApplicationMessageToUser(
                        $"{count++} ) {pi.Name} : {pi.GetValue(request, null)}"
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
        /// Show all requests in request dictionary
        /// </summary>
        protected void ShowRequestList()
        {
            var count = 1;
            foreach (var request in ProgramStorageQueries.GetRequests())
            {
                CMD.ShowApplicationMessageToUser($"{count++} )key : {request.Key} \tvalue : {request.Value.RequestUri}");
            }
        }

        /// <summary>
        /// Show all cookie in cookie container for specific request
        /// </summary>
        /// <param name="key">Request key</param>
        /// <param name="url">Cookie domain</param>
        protected void ShowRequestCookieContainer(string url,string key)
        {
            try
            {
                var count = 1;
                foreach (Cookie cookie in ProgramStorageQueries.GetCookieContainer(key).GetCookies(new Uri(url)))
                {
                    CMD.ShowApplicationMessageToUser($"{count++} ) [name : {cookie.Name}] [value : {cookie.Value}] [path : {cookie.Path}] [domain : {cookie.Domain}]");
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// Show current request
        /// </summary>
        protected void ShowCurrentRequest()
        {
            try
            {
                var request = ProgramStorageQueries.GetCurrentRequest();
                var key = ProgramStorageQueries.GetRequestKey(request);
                CMD.ShowApplicationMessageToUser($"{key}\t{request.RequestUri}");
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// display request proxy
        /// </summary>
        /// <param name="key">key of request</param>
        protected void DisplayRequestProxy(string key = null)
        {
            try
            {
                var request = ProgramStorageQueries.GetRequest(key);
                CMD.ShowApplicationMessageToUser($"{request.Proxy.GetProxy(request.RequestUri)}");
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

    }
}
