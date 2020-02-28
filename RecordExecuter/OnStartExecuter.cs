namespace RecordExecuter {
    public class OnStartExecuter {
        readonly int threadId = -1;
        public OnStartExecuter (string recordsInJsonFormat) {

            Storage.LoadCombo ("ComboList.txt");
            Storage.LoadProxy ("ProxyList.txt");

            //create instace of thread guid and set in session
            var threadGuid = new ThreadGuid ();
            //set original in that
            threadGuid.OriginalJson = recordsInJsonFormat;
            //get records from json
            var records = Tools.SetCorrectFormat (recordsInJsonFormat);
            //add new guid for own
            threadGuid.AddNewGuid (threadId, records);
            //just for simulation session
            Storage.Guid = threadGuid;
            //execute records
            Tools.RunRecords (records);
        }
    }
}