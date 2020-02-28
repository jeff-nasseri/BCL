using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCL.Request {
    class ManagerAction {
        protected void GenerateNewRequest (string key, string url) {
            try {
                ProgramStorageQueries.AddNewRequest (key, url);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }
        protected void ActivateRequest (string key) {
            try {
                ProgramStorageQueries.SetCurrentRequest (ProgramStorageQueries.GetRequest (key) ??
                    throw new Exception ("key not valid"));
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }
    }
}