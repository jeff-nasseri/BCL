using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Records
{
    [ClassCommandInfo("rec_view")]
    class _View : ViewConfig
    {
        [ClassCommandInfo("list", "display history of user input commands")]
        public void _DisplayRecords()
        {
            DisplayCommandsInformation();
        }

        [ClassCommandInfo("state","display state of record")]
        public void _DisplayStateOfRecord()
        {
            DisplayStateOfRecord();
        }

    }
}
