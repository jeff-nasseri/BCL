using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace BCL.Records {
    class ConfigAction {

        /// <summary>
        /// remove specific command information
        /// </summary>
        /// <param name="key">key of command information</param>
        public void RemoveRecord (string key) {
            try {
                if (key.Contains (',')) {
                    foreach (var item in key.Split (',')) {
                        RecordQueries.RemoveCommand (Int32.Parse (item));
                    }
                    return;
                }
                RecordQueries.RemoveCommand (Int32.Parse (key));
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// remove range of commands from commands information by commands key
        /// </summary>
        /// <param name="prefix"></param>
        public void RemoveRecords (string prefix) {
            try {
                var keys = RecordQueries.GetKeyOfRecords (prefix);
                RecordQueries.RemoveRecords (keys);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// clear all commands from commands information
        /// </summary>
        public void ClearRecords () {
            try {
                RecordQueries.ClearRecords ();
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// pause current reconding
        /// </summary>
        /// <param name="state">state of record</param>
        public void PauseRecord (string state) {
            switch (state) {
                case "play":
                    RecordQueries.SetRecordState (true);
                    break;
                case "pause":
                    RecordQueries.SetRecordState (false);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="position"></param>
        /// <param name="value"></param>
        public void RecordDynamicize (string key, string position, string value) {
            try {
                var record = RecordQueries.GetRecord (Int32.Parse (key));
                record.Value.args[Int32.Parse (position)] = value;
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// copy records to variable
        /// </summary>
        /// <param name="name">name of variable</param>
        public void CopyRecords (string name) {
            try {
                var data = RecordQueries.GetNewInstanceOfRecords ().Select (item => new {
                    Name = item.Key,
                        Value = item.Value
                });
                VariablesStorageQueries.AddNewVariable (name, data);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// paste records in records list
        /// </summary>
        /// <param name="name">name of variable</param>
        public void PasteRecords (string name) {
            try {
                var records = VariablesStorageQueries.GetVariableValue (name) as IEnumerable<object>;
                foreach (dynamic item in records) {
                    RecordQueries.AddNewRecord (item.Name, item.Value);
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        /// <summary>
        /// export records to text file
        /// </summary>
        /// <param name="name">file name</param>
        public void ExportRecords (string fileName) {
            var records = RecordQueries.GetRecords ();
            using (StreamWriter writer = new StreamWriter (fileName == "" ? Guid.NewGuid ().ToString () + "_records.txt" : fileName)) {
                var listOfRecordModel = RecordQueries.GetRecords ().Select (rec => new RecordModel {
                    Id = rec.Key,
                        ModelInstanceName = rec.Value.modelInstance.ToString (),
                        MethodName = rec.Value.method.Name,
                        args = rec.Value.args,
                        ReturnedName = rec.Value.method.Name + '-' + rec.Key,
                        Once = false
                });
                var Json = JsonConvert.SerializeObject (listOfRecordModel);
                writer.Write (Json);
            }
        }
        /// <summary>
        /// import records to records storage
        /// </summary>
        /// <param name="name">file name</param>
        public void ImportRecords (string fileName) {
            try {
                using (StreamReader reader = new StreamReader (fileName)) {
                    var json = reader.ReadToEnd ();
                    var records = JsonConvert.DeserializeObject<List<RecordModel>> (json) as List<RecordModel>;
                    //add to record storage
                    foreach (var record in records) {
                        RecordQueries.AddNewRecord (record.Id, (Type.GetType (record.ModelInstanceName),
                            Type.GetType (record.ModelInstanceName).GetMethod (record.MethodName), record.args));
                    }
                }
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        public void SetCommandInArg (string targetCommandId, string argIndex, string commandId, string customeArg) {
            try {
                //initial data
                var data = string.Empty;
                //get command func
                Func<RecordModel> func = () => {
                    var record = RecordQueries.GetRecord (Int32.Parse (commandId));
                    return new RecordModel () {
                        Id = record.Key,
                            args = record.Value.args,
                            MethodName = record.Value.method.Name,
                            Once = false,
                            ModelInstanceName = record.Value.modelInstance.ToString (),
                            ReturnedName = record.Value.method.Name + '-' + record.Key
                    };
                };
                //validation
                var result = commandId == "" ? data = customeArg : data = JsonConvert.SerializeObject (func ());
                //get target
                var targetCommand = RecordQueries.GetRecord (Int32.Parse (targetCommandId));
                targetCommand.Value.args[Int32.Parse (argIndex)] = data;
                //update arg
                System.Console.WriteLine ();
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }

        public void ExecuteRecords (string fileName) {
                using (StreamReader reader = new StreamReader (fileName)) {
                    var json = reader.ReadToEnd ();
                    var records = JsonConvert.DeserializeObject<List<RecordModel>> (json) as List<RecordModel>;
                    Utilities.RunRecords (records);
                }
        }
        
    }
}