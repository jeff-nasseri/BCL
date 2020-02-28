using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Utility
{
    class ViewAction
    {

        /// <summary>
        /// Displays default headers for when display request headers
        /// </summary>
        public void ShowDefaultRequestHeaders()
        {
            var count = 0;
            for(int i = 0; i<Utilities.DefaultRequestShowableHeaders.Length; i++)
            {
                CMD.ShowApplicationMessageToUser($"{count++} ) {Utilities.DefaultRequestShowableHeaders[i]}");
            }
        }

        /// <summary>
        /// Displays default headers for when display response headers
        /// </summary>
        public void ShowDefaultResponsetHeaders()
        {
            var count = 0;
            for (int i = 0; i < Utilities.DefaultResponseShowableHeaders.Length; i++)
            {
                CMD.ShowApplicationMessageToUser($"{count++} ) {Utilities.DefaultRequestShowableHeaders[i]}");
            }
        }

        /// <summary>
        /// Show all restart words
        /// </summary>
        protected void ShowRestartWords()
        {
            var count = 0;
            foreach(var word in Utilities.RestartCommandWords)
            {
                CMD.ShowApplicationMessageToUser($"{count++} ) {word}");
            }
        }

        /// <summary>
        /// Show all clear words
        /// </summary>
        public void ShowClearConsoleWords()
        {
            var count = 0;
            foreach(var word in Utilities.ClearConsoleWords)
            {
                CMD.ShowApplicationMessageToUser($"{count++} ) {word}");
            }
        }
    }
}
