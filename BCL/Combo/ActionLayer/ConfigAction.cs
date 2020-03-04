using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Combo {
    class ConfigAction {
        /// <summary>
        /// set combo list
        /// </summary>
        /// <param name="path">combo list file path</param>
        public void SetComboList (string path) {
            try {
                var list = Utilities.ReadFile (path) as List<string>;
                LooperQueries.SetComboList (list);
            } catch (Exception e) {
                CMD.ShowApplicationMessageToUser ($"message : {e.Message}\nroute : {this.ToString()}", showType : ShowType.DANGER);
            }
        }
        //#style
        public void ValidateCombo(string is_contain,string combo){
        }
    }
}