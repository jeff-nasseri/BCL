using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Records
{
    class ViewConfig
    {

        /// <summary>
        /// display history of user input commands
        /// </summary>
        public void DisplayCommandsInformation()
        {
            try
            {
                var commands = RecordQueries.GetRecords();
                foreach (var command in commands)
                {
                    var value = command.Value;
                    string str = "";
                    foreach (var item in value.args)
                    {
                        if (item != null)   
                        {
                            str += $"{item.ToString()},";
                        }
                    }
                    CMD.ShowApplicationMessageToUser($"{command.Key}\t{value.modelInstance}\t{value.method}\t[{str}]");
                }
            }
            catch (Exception e)
            {
                CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
            }
        }

        /// <summary>
        /// display record state
        /// </summary>
        public void DisplayStateOfRecord()
        {
            CMD.ShowApplicationMessageToUser($"record state : {RecordQueries.GetRecordState()}");
        }


    }
}
