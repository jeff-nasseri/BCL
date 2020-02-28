using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Utility
{
    [ClassCommandInfo("utl_view")]
    class _View : ViewAction
    {
        
        
        [ClassCommandInfo("req_headers","show default headers must show in prop command in the req_view command class")]
        public void _ShowDefaultRequestHeaders()
        {
            ShowDefaultRequestHeaders();
        }
        
        
        [ClassCommandInfo("res_headers", "show default headers must show in prop command in the res_view command class")]
        public void _ShowDefaultResponsetHeaders()
        {
            ShowDefaultResponsetHeaders();
        }

        [ClassCommandInfo("restart_words", "show default restart words for restart program abd clear static storage")]
        public void _ShowRestartWords()
        {
            ShowRestartWords();
        }

        [ClassCommandInfo("clear_console_words", "show default restart words for restart program abd clear static storage")]
        public void _ShowClearConsoleWords()
        {
            ShowClearConsoleWords();
        }

    }
}
