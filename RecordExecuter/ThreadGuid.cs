using System.Collections.Generic;
using System.Linq;

namespace RecordExecuter {
    public class ThreadGuid {
        public Dictionary<int, List<RecordModel>> GuidList = new Dictionary<int, List<RecordModel>> ();
        public string OriginalJson { get; set; }
        public List<RecordModel> SetToDefault (int guidId, string recordsInJsonFormat) {
            var records = GetGuid (guidId).Value;
            records = Tools.SetCorrectFormat (recordsInJsonFormat);
            return records;
        }
        public KeyValuePair<int, List<RecordModel>> AddNewGuid (int guidId, List<RecordModel> records) {
            GuidList.Add (guidId, records);
            return GetGuid (guidId);
        }
        public KeyValuePair<int, List<RecordModel>> GetGuid (int id) {
            return GuidList.Single (guid => guid.Key == id);
        }
        public bool RemoveGuidFromGuidList (int guidId) {
            GuidList.Remove (guidId);
            return true;
        }
    }
}