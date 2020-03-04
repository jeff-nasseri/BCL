using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Combo {
    [ClassCommandInfo ("combo_config")]
    class _Config : ConfigAction {
        [ClassCommandInfo ("set", "set combo list", argsInfo : new string[] { "path=>combo file path" })]
        public void _SetComboList (string path) {
            SetComboList (path);
        }

        [ClassCommandInfo ("save_combo", "validate combo for save in session by is_contain function")]
        public void _ValidateCombo (string is_contain, string combo) {
            ValidateCombo (is_contain, combo);
        }
    }
}