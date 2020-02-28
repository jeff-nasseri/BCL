using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace BCL {
    public class RecordQueries : RecordStorage {
        static int counter = 0;
        public static int key {
            get {
                if (Records.Count () > 0) {
                    counter = Records.OrderBy(r=>r.Key).Last().Key;
                }
                return ++counter;
            }
        }
        /// <summary>s
        /// add user command to command list for recording
        /// </summary>
        /// <param name="info">information of current command</param>
        public static void AddNewCommad ((object modelInstance, MethodInfo method, object[] args) info) {
            try {
                Records.Add (key, info);
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// return list of command information
        /// </summary>
        public static IDictionary < int, (object modelInstance, MethodInfo method, object[] args) > GetRecords () {
            return Records;
        }

        /// <summary>
        /// set records by data
        /// </summary>
        /// <param name="data">data of records</param>
        public static void SetRecords (IDictionary < int, (object modelInstance, MethodInfo method, object[] args) > data) {
            Records = data;
        }

        /// <summary>
        /// get specific record in records list
        /// </summary>
        /// <param name="key">key of record</param>
        public static KeyValuePair < int, (object modelInstance, MethodInfo method, object[] args) > GetRecord (int key) {
            return Records.Single (rec => rec.Key == key);
        }

        /// <summary>
        /// add new record
        /// </summary>
        /// <param name="key">key of record</param>
        /// <param name="value">record value</param>
        public static void AddNewRecord (int key, (object modelInstance, MethodInfo method, object[] args) value) {
            Records.Add (key, value);
        }

        /// <summary>
        /// return new instance of record list
        /// </summary>
        public static IDictionary < int, (object modelInstance, MethodInfo method, object[] args) > GetNewInstanceOfRecords () {
            var instance = new Dictionary < int,
                (object modelInstance, MethodInfo method, object[] args) > ();
            foreach (var item in Records) {
                instance.Add (item.Key, item.Value);
            }
            return instance;
        }

        /// <summary>
        /// remove command from commands information llist
        /// </summary>
        /// <param name="key">command information key</param>
        public static void RemoveCommand (int key) {
            Records.Remove (key);
        }

        /// <summary>
        /// return list of commands key whitch contain by prefix
        /// </summary>
        /// <param name="prefix"></pazram>
        public static List<int> GetKeyOfRecords (string prefix) {
            return Records.Where (ci => ci.Value.modelInstance.ToString ().ToLower ().Contains (prefix.ToLower ())).Select (ci => ci.Key).ToList ();
        }

        /// <summary>
        /// remove commands by key
        /// </summary>
        /// <param name="keys">commands key list</param>
        public static void RemoveRecords (List<int> keys) {
            foreach (var key in keys) {
                Records.Remove (key);
            }
        }

        /// <summary>
        /// clear all commands from commands information
        /// </summary>
        public static void ClearRecords () {
            Records.Clear ();
        }

        /// <summary>
        /// validation for specific command information
        /// </summary>
        /// <param name="key">key of commmand information</param>
        public static bool IsRecordValid (int key) {
            return Records.Any (ci => ci.Key == key);
        }

        /// <summary>
        /// get particular command information by key
        /// </summary>
        /// <param name="key">key of command information</param>
        public static (object modelInstance, MethodInfo method, object[] args) GetRecordValue (int key) {
            var result = IsRecordValid (key) ? Records.Single (ci => ci.Key == key).Value : throw new Exception ("command not found!");
            return result;
        }

        /// <summary>
        /// get state of record true:play , pause:stop
        /// </summary>
        public static bool GetRecordState () {
            return RecordState;
        }

        /// <summary>
        /// set state of record true:play , pause:stop
        /// </summary>
        public static void SetRecordState (bool state) {
            RecordState = state;
        }
    }
}