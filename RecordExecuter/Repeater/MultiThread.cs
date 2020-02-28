using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RecordExecuter.Repeater {
    public class MultiThread {
        int threadId_dynamic { get; set; }
        readonly int threadId_fix;
        // readonly string recordsInJsonFormat;
        readonly List<RecordModel> records;
        public MultiThread (int threadId, string recordsInJsonFormat) {
            threadId_fix = threadId_dynamic = threadId;
            records = Tools.SetCorrectFormat (recordsInJsonFormat);
            var guid = Storage.Guid.AddNewGuid (threadId, records);

            while (true) {
                Tools.RunRecords (records);
                records = Storage.Guid.SetToDefault (threadId_fix, recordsInJsonFormat);
                threadId_dynamic++;
            }

        }
    }
}