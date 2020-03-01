using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace RecordExecuter.Repeater {
    public class MultiThread {
        readonly int threadId_fix;
        List<RecordModel> records { get; set; }
        readonly string recordsInJsonFormat;
        public MultiThread (int threadId, string recordsInJsonFormat) {
            threadId_fix = threadId;
            this.recordsInJsonFormat = recordsInJsonFormat;
            records = Tools.SetCorrectFormat (recordsInJsonFormat);
            var guid = Storage.Guid.AddNewGuid (threadId, records);
        }
        public void Run (object callback) {
            while (true) {
                Tools.RunRecords (records);
                records = Storage.Guid.SetToDefault (threadId_fix, recordsInJsonFormat);
            }
        }
    }
}