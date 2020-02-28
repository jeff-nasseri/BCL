using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace RecordExecuter {
    public class Tools {
        public static object[] SetArg (List<RecordModel> records, object[] args) {
            for (int i = 0; i < args.Length; i++) {
                if (records.Any (item => args[i].ToString () == item.ReturnedName)) {
                    var value = records.Single (item => args[i].ToString () == item.ReturnedName).Value;
                    // args[i] = AnalysisValue (value, args[i], i);
                    args[i] = value;
                }
            }
            return args;
        }
        public static object AnalysisValue (object value, object arg, int id) {
            var _arg = arg.ToString ();
            if (_arg.Contains (":")) {
                var _0 = _arg.Split (':') [0];
                var _1 = _arg.Split (':') [1];
                var data = (value as IEnumerable<object>);
                var _2 = data.ElementAt (Int32.Parse (_1));
                return _2;
            }
            return value;
        }
        public static List<RecordModel> SetCorrectFormat (string records_json) {
            var records = JsonConvert.DeserializeObject<List<RecordModel>> (records_json);
            foreach (var record in records) {
                record.ModelInstanceName = record.ModelInstanceName.Replace ("BCL", "RecordExecuter");
            }
            return records;
        }
        public static void RunRecords (List<RecordModel> records) {

            foreach (var record in records) {
                var Id = record.Id;
                var type = Type.GetType (record.ModelInstanceName);
                var Method = type.GetMethod (record.MethodName);
                var args = record.args;
                var modelInstance = Activator.CreateInstance (type);
                record.Value = Method.Invoke (modelInstance, SetArg (records, args));
                Storage.Objects.Add (record.Value);
            }
        }

        //no longer been here
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

    }
}