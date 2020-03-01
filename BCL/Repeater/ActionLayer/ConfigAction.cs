using System;
using System.Linq;
using Newtonsoft.Json;

namespace BCL.Repeater {
    public class ConfigAction {
        public void Repeat (string hasThread, string number, string threadNumber) {
            try {

                var data = bool.Parse (hasThread) ? Int32.Parse (threadNumber) : Int32.Parse (number);

                var record_last = RecordQueries.GetRecords ().Last ();
                RecordQueries.GetRecords ().Remove (record_last.Key);
                var last_id = record_last.Key;
                var _id=RecordQueries.GetRecords ().OrderBy (r=>r.Key).Last().Key;
                var listOfRecordModel = RecordQueries.GetRecords ().Where (r => r.Key <=_id )
                    .Select (rec => new RecordModel {
                        Id = rec.Key,
                            ModelInstanceName = rec.Value.modelInstance.ToString (),
                            MethodName = rec.Value.method.Name,
                            args = rec.Value.args,
                            ReturnedName = rec.Value.method.Name + '-' + rec.Key,
                            Once = false
                    });
                var records_json = JsonConvert.SerializeObject (listOfRecordModel);
                var modelInstance = Type.GetType ("BCL.Repeater._Config");
                var method = modelInstance.GetMethod ("_Repeat");
                var args = new object[] { hasThread, data.ToString(), records_json };
                RecordQueries.AddNewRecord (last_id, (modelInstance, method, args));

            } catch (System.Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }
    }
}