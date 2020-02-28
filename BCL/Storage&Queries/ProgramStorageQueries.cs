using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Net;
using System.IO;

namespace BCL
{
    public class ProgramStorageQueries : ProgramStorage
    {

        /// <summary>
        /// Get value of particular feature
        /// </summary>
        /// <param name="featuresName">Feature name</param>
        /// <returns>Value of feature</returns>
        public static IEnumerable<(string, object)> GetFeatureInfo(ClassesCommandFeaturesName featuresName)
        {
            if (IsFeatureExist(featuresName))
                return ClassCommandInfo.Where(tuple => tuple.Item1 == featuresName).Select(tuple => tuple.Item2);
            else
                throw new Exception("No feature found with this feature name");
        }

        /// <summary>
        /// Validation for feature name in class command info list
        /// </summary>
        /// <param name="featureName">Name of feature</param>
        public static bool IsFeatureExist(ClassesCommandFeaturesName featureName)
        {
            return ClassCommandInfo.Any(item => item.Item1 == featureName);
        }

        /// <summary>
        /// Find class name by name of class command
        /// </summary>
        /// <param name="classCommand">Name of class command</param>
        /// <returns>name of class that has same classCommand</returns>
        public static string GetClassName(string classCommand)
        {
            var feature = GetFeatureInfo(ClassesCommandFeaturesName.ClassesCommand);
            if (IsClassCommandExist(classCommand))
                return feature.Single(item => (item.Item2 as string) == (classCommand)).Item1;
            else
                throw new Exception("No class command found !");
        }

        /// <summary>
        /// Find value of particular information of particular feature
        /// </summary>
        /// <param name="feature">Name of feature</param>
        /// <param name="className">Name of class</param>
        public static object GetInfoValue(IEnumerable<(string, object)> feature, string className)
        {
            if (IsClassNameExist(feature, className))
                return feature.SingleOrDefault(t => (t.Item1 as string) == (className)).Item2;
            else
                throw new Exception("No class name found !");
        }

        /// <summary>
        /// Validation class name for particular feature
        /// </summary>
        /// <param name="feature">Name od feature</param>
        /// <param name="className">Name od class</param>
        public static bool IsClassNameExist(IEnumerable<(string, object)> feature, string className)
        {
            return feature.Any(item => item.Item1 == className);
        }

        /// <summary>
        /// Get functions command of by class name
        /// </summary>
        /// <param name="className">Name of class</param>
        /// <returns>List of functions command</returns>
        public static IEnumerable<string> GetFunctionsCommand(string className)
        {
            var feature = GetFeatureInfo(ClassesCommandFeaturesName.AppsList);
            if (IsClassNameExist(feature, className))
            {
                var app = GetInfoValue(feature, className) as CommandLineApplication;
                foreach (var item in app.Commands)
                {
                    yield return item.Name;
                }
            }
            else
            {
                throw new Exception("No class name found");
            }
        }

        /// <summary>
        /// Get request by key
        /// </summary>
        /// <param name="key">Request key</param>
        public static HttpWebRequest GetRequest(string key)
        {
            return RequestDictionary.SingleOrDefault(req => req.Key == key).Value ?? (CurrentRequest ?? throw new Exception("Current request is empty!"));
        }

        /// <summary>
        /// Validation for request by request key in rest dictionary
        /// </summary>
        /// <param name="key">Key of request</param>
        public static bool IsRequestExist(string key)
        {
            return RequestDictionary.Any(item => item.Key == key);
        }

        /// <summary>
        /// Dictionary of paire request and key
        /// </summary>
        public static IDictionary<string, HttpWebRequest> GetRequests()
        {
            return RequestDictionary;
        }

        /// <summary>
        /// Get current enabled request
        /// </summary>
        public static HttpWebRequest GetCurrentRequest()
        {
            return CurrentRequest ?? throw new Exception("Current request is empty!");
        }

        /// <summary>
        /// Get current enabled response
        /// </summary>
        public static HttpWebResponse GetCurrentResponse()
        {
            return CurrentResponse ?? throw new Exception("Current response is empty!");
        }

        /// <summary>
        /// List of info value for class command feature
        /// </summary>
        public static IEnumerable<object> GetInfoValueOfClassCommand()
        {
            return GetFeatureInfo(ClassesCommandFeaturesName.ClassesCommand).Select(t => t.Item2);
        }

        /// <summary>
        /// Check list of commands(class command) for specific command
        /// </summary>
        public static bool IsClassCommandExist(string command)
        {
            return GetInfoValueOfClassCommand().Any(r => (r as string) == (command));
        }

        /// <summary>
        /// Check list of commands(function command) for specific command
        /// </summary>
        public static bool IsFunctionCommand(string command)
        {
            return FunctionsCommand.Any(array => (array as string[]).Any(c => c == (command)));
        }

        /// <summary>
        /// Get response by key
        /// </summary>
        /// <param name="key">Response key</param>
        public static HttpWebResponse GetResponse(string key)
        {
            return ResponsetDictionary.SingleOrDefault(res => res.Key == key).Value ?? CurrentResponse ?? throw new Exception("No response found!");
        }

        /// <summary>
        /// Check response dictionary for particular response by response key
        /// </summary>
        /// <param name="key">Key of response</param>
        public static bool IsResponseExist(string key)
        {
            return ResponsetDictionary.Any(item => item.Key == key);
        }

        /// <summary>
        /// List of response
        /// </summary>
        public static IDictionary<string, HttpWebResponse> GetResponses()
        {
            return ResponsetDictionary;
        }

        /// <summary>
        /// Get key of response by web response object
        /// </summary>
        /// <param name="response">http response object</param>
        public static string GetResponseKey(HttpWebResponse response)
        {
            if (IsResponseExist(response))
                return ResponsetDictionary.SingleOrDefault(res => res.Value == response).Key;
            else
                throw new Exception("No response found!");
        }

        /// <summary>
        /// Check response dictionary for particular response by response object 
        /// </summary>
        /// <param name="response">response object</param>
        public static bool IsResponseExist(HttpWebResponse response)
        {
            return ResponsetDictionary.Any(item => item.Value == response);
        }

        /// <summary>
        /// Get key of request by web request object
        /// </summary>
        /// <param name="request">request object</param>
        public static string GetRequestKey(HttpWebRequest request)
        {
            if (IsRequestExist(request))
                return RequestDictionary.Single(res => res.Value == request).Key;
            else
                throw new Exception("No request found!");
        }

        /// <summary>
        /// Check request dictionary for particular request by request object 
        /// </summary>
        /// <param name="request">request object</param>
        public static bool IsRequestExist(HttpWebRequest request)
        {
            return RequestDictionary.Any(item => item.Value == request);
        }

        /// <summary>
        /// validation for current request
        /// </summary>
        public static bool IsCurrentRequestValid()
        {
            return CurrentRequest != null;
        }


        /// <summary>
        /// Get stream of response
        /// </summary>
        /// <param name="key">Response key</param>
        public static Stream GetResponseStream(string key)
        {
            return ResponsesStream.SingleOrDefault(item => item.Item1 == key).Item2 ??
                ResponsesStream.SingleOrDefault(item => item.key == GetResponseKey(CurrentResponse)).stream ??
                throw new Exception("No response found!");
        }

        /// <summary>
        /// Get cookie container by key
        /// </summary>
        /// <param name="key">container key</param>
        public static CookieContainer GetCookieContainer(string key)
        {

            return CookieContainerList.SingleOrDefault(cc => cc.key == key).container ??
                (CookieContainerList.SingleOrDefault(cc => cc.key == GetRequestKey(CurrentRequest)).container ??
                throw new Exception("No container found!"));

        }

        /// <summary>
        /// Remove request from request dictionary
        /// </summary>
        /// <param name="key">Request key</param>
        public static void RemoveRequest(string key)
        {
            if (key == null)
            {
                RemoveCurrentRequest();
            }
            else
            {
                if (IsRequestExist(key))
                    ProgramStorage.RequestDictionary.Remove(key);
                else
                    throw new Exception("No request found!");
            }
        }

        /// <summary>
        /// Remove current enabled request
        /// </summary>
        public static void RemoveCurrentRequest()
        {
            ProgramStorage.CurrentRequest = null;
        }

        /// <summary>
        /// Get list of functions command
        /// </summary>
        public static IEnumerable<string[]> GetFunctionCommand()
        {
            return ProgramStorage.FunctionsCommand;
        }


        public static void SetFunctionsCommand(IEnumerable<string[]> commands)
        {
            FunctionsCommand = commands;
        }


        public static void SetCurrentRequest(HttpWebRequest request)
        {
            CurrentRequest = request;
        }


        public static void CreateNewResponse(string key)
        {
            ResponsetDictionary.Add(key, (HttpWebResponse)GetRequest(key).GetResponse());
        }


        public static void SaveResponseStream(string key, Stream stream)
        {
            ResponsesStream.Add((key, stream));
        }


        public static void SaveRequestStream(string key)
        {
            RequestStreams.Add((key, Utilities.CopyAndClose(GetRequest(key).GetRequestStream())));
        }


        public static void SetCurrentresponse(HttpWebResponse response)
        {
            CurrentResponse = response;
        }

        /// <summary>
        /// Add new request to request dictionary by key and add new cookie container to container dictionary wuth 
        /// that key
        /// </summary>
        /// <param name="key">Request key</param>
        /// <param name="url">Request url</param>
        public static void AddNewRequest(string key, string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            RequestDictionary.Add(key, req);
            //create new cookicontainer for each request
            CookieContainerList.Add((key, new CookieContainer()));
        }


        public static void AddNewClassCommandInfo((ClassesCommandFeaturesName, (string, object)) info)
        {
            ClassCommandInfo.Add(info);
        }


    }
}
