using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BCL.Response
{
    class ManagerAction
    {
        protected void CreateNewResponse(string key)
        {
            try
            {
                ProgramStorageQueries.CreateNewResponse(key);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }
        protected void ActivateResponse(string key)
        {
            try
            {
                var response = ProgramStorageQueries.GetResponse(key);
                if (response == null)
                {
                    throw new Exception("response not valid");
                }
                ProgramStorageQueries.SetCurrentresponse(response);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }
        protected void SaveReponseStream(string key)
        {
            try
            {
                var response = ProgramStorageQueries.GetResponse(key).GetResponseStream();
                var stream = Utilities.CopyAndClose(response);
                ProgramStorageQueries.SaveResponseStream(key, stream);
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"________message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }
    }
}
