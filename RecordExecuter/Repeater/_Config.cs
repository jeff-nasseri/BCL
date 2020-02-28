using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace RecordExecuter.Repeater {
    public class _Config {
        readonly int threadId = -2;
        public void _Repeat (string hasThread, string data, string recordsInJsonFormat) {

            var records = Tools.SetCorrectFormat (recordsInJsonFormat);
            var guid = Storage.Guid.AddNewGuid (threadId, records);

            for (int i = 0; i < Int32.Parse (data); i++) {
                if (bool.Parse (hasThread)) {
                    Thread.Sleep (200);
                    new Thread (new ThreadStart (() => new MultiThread (i, recordsInJsonFormat))).Start ();
                } else {
                    Tools.RunRecords (records);
                    records=Storage.Guid.SetToDefault (threadId, recordsInJsonFormat);
                }
            }
        }
    }
}