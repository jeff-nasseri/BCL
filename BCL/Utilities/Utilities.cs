using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BCL {
    public class Utilities {
        /*
         * add::name                insert
         * var::name                get
         * Accept->*                header
         * Accept->*,,Method->GET   headers
         * key=>request key         argument description 
         */
        public const string Mode_1 = ",,";
        public const string Mode_2 = "->";
        public const string Mode_3 = "::";
        public const string Mode_4 = "_";
        public const string Mode_5 = "=>";

        public static string[] RestartCommandWords = { "exit", "restart", "base" };
        public static string[] ClearConsoleWords = { "clr", "clear" };
        public static string[] DefaultRequestShowableHeaders = {
            "Method",
            "ContentType",
            "CookieContainer",
            "ContentLength",
            "Proxy",
            "Referer",
            "Accept",
            "Headers",
            "KeepAlive",
            "Host",
            "UserAgent"
        };
        public static string[] DefaultResponseShowableHeaders = { "ContentLength", "ContentType", "ContentEncoding", "Cookies", "Headers", "Method" };

        /// <summary>
        /// Make array from sentence in specific format
        /// </summary>
        /// <param name="input">Sentence</param>
        /// <param name="mode">Mode_n(0<n<6)</param>
        /// <param name="toLowwer">If set to true array member are overwritten in lowercase</param>
        public static string[] GetArray (string input, string mode, bool toLowwer = false) {
            var length = input.Split (mode).Length;
            var array = new string[length];
            for (int i = 0; i < length; i++) {
                array[i] = input.Split (mode) [i];
            }
            return toLowwer ? array.Select (str => str.ToLower ()).ToArray () : array;
        }

        /// <summary>
        /// Copy stream and close input stream
        /// </summary>
        /// <param name="inputStream">Current stream</param>
        public static Stream CopyAndClose (Stream inputStream) {
            const int readSize = 256;
            byte[] buffer = new byte[readSize];
            MemoryStream ms = new MemoryStream ();

            int count = inputStream.Read (buffer, 0, readSize);
            while (count > 0) {
                ms.Write (buffer, 0, count);
                count = inputStream.Read (buffer, 0, readSize);
            }
            ms.Position = 0;
            inputStream.Close ();
            return ms;
        }

        /// <summary>
        /// return utility command if command utility
        /// </summary>
        public static string GetUtilitiesCommand (string command) {
            return RestartCommandWords.Any (str => str.Contains (command)) ? "restart_route" :
                ClearConsoleWords.Any (str => str.Contains (command)) ? "clear_console" : "not found";
        }

        /// <summary>
        /// return specific substring from sentence
        /// </summary>
        /// <param name="sentence">Current sentence</param>
        /// <param name="startStr">Start string from sentence for create substring</param>
        /// <param name="endStr">This paramater specify end string in sentence for create substring</param>
        /// <param name="startNum">Integer value that remove chars from start of substring</param>
        /// <param name="endNum">Integer value that remove chars from end of substring</param>
        public static string GetSubstring (string sentence, string startStr, string endStr, string startNum, string endNum) {
            int startIndex = sentence.IndexOf (startStr);
            int endIndex = sentence.IndexOf (endStr);
            var result = sentence.Substring (startIndex + (int.Parse (startNum)), (endIndex - startIndex) - int.Parse (endNum));
            return result;
        }

        /// <summary>
        /// Remove current line of cmd from start index
        /// </summary>
        /// <param name="startIndex">Start position for clear chars</param>
        public static void ClearCurrentInput (int startIndex = 0) {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition (startIndex, Console.CursorTop);
            Console.Write (new string (' ', Console.WindowWidth));
            Console.SetCursorPosition (startIndex, currentLineCursor);
        }

        /// <summary>
        /// Remove chars from string bulder ,copy and paste to (to)
        /// </summary>
        /// <param name="builder">String builder</param>
        /// <param name="sourceIndex">From source index</param>
        /// <param name="count">Lentgh</param>
        /// <param name="to">Target for paste chars</param>
        public static void CopyAndRemoveChars (StringBuilder builder, int sourceIndex, int count, out char[] to) {
            to = new char[count];
            builder.CopyTo (sourceIndex, to, count);
            builder.Remove (sourceIndex, count);
        }

        /// <summary>
        /// Found match for autocompletion (Tab)
        /// </summary>
        /// <param name="builder">String builder</param>
        /// <param name="data">List of string represent data</param>
        /// <returns>The closest word that is most similar to builder</returns>
        public static string FindMatch (StringBuilder builder, IEnumerable<string> data) {
            var currentInput = builder.ToString ();
            var match = data.FirstOrDefault (item => item != currentInput && item.StartsWith (currentInput, true, CultureInfo.InvariantCulture));
            return match;
        }

        /// <summary>
        /// Invoke method by reflection .method must not be void
        /// </summary>
        /// <typeparam name="T">The class that contains the method</typeparam>
        /// <typeparam name="R">The type that method result convert to</typeparam>
        /// <param name="constructor">Constructor of type T</param>
        /// <param name="methodName">Name of method</param>
        /// <param name="parameters">Method arguments</param>
        /// <param name="result">The variable in which the result of the method is stored</param>
        public static void MethodInvoker<T, R> (T constructor, string methodName, object[] parameters, out R result) {
            var method = typeof (T).GetMethod (methodName);
            result = (R) Convert.ChangeType (method.Invoke (constructor, parameters), typeof (R));
        }

        /// <summary>
        /// Invoke generic method by reflection .method must not be void
        /// </summary>
        /// <typeparam name="T">The class that contains the method</typeparam>
        /// <typeparam name="G">generic type</typeparam>
        /// <typeparam name="R">The type that method result convert to</typeparam>
        /// <param name="constructor">Constructor of type T</param>
        /// <param name="methodName">Name of method</param>
        /// <param name="parameters">Method arguments</param>
        /// <param name="result">The variable in which the result of the method is stored</param>
        public static void GenericInvoker<T, G, R> (T constructor, string methodName, object[] parameters, out R result) {
            var method = typeof (T).GetMethod (methodName);
            var generic = method.MakeGenericMethod (typeof (G));
            result = (R) Convert.ChangeType (generic.Invoke (constructor, parameters), typeof (R));
        }

        /// <summary>
        /// read all line from file
        /// </summary>
        /// <param name="path">file path</param>
        public static IEnumerable<string> ReadFile (string path) {
            List<string> list = new List<string> ();
            using (StreamReader reader = new StreamReader (path)) {
                string line = "";
                while (line != null) {
                    line = reader.ReadLine ();
                    if (!string.IsNullOrEmpty (line)) {
                        list.Add (line);
                    }
                }
            }
            return list.Where (i => i != null /* add another condition here like : && i==string.Empty()*/ ).Distinct ().ToList ();
        }

        /// <summary>
        /// write ienumerable of T type in file
        /// </summary>
        /// <typeparam name="T">type of data</typeparam>
        /// <param name="path">path for write file</param>
        /// <param name="data">data for write</param>
        public static void WriteFile<T> (string path, IEnumerable<T> data) {
            using (StreamWriter writer = new StreamWriter (path)) {
                foreach (var item in data) {
                    writer.WriteLine (item);
                }
            }
        }

        public static void RunRecords (List<RecordModel> records) {
            foreach (var record in records) {
                var Id = record.Id;
                var type = Type.GetType (record.ModelInstanceName);
                var Method = type.GetMethod (record.MethodName);
                var args = record.args;
                var modelInstance = Activator.CreateInstance (type);
                Method.Invoke (modelInstance, args);
            }
        }

    }
}