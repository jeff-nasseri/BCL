using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Combo
{
    [ClassCommandInfo("combo_config")]
    class _Config : ConfigAction
    {
        [ClassCommandInfo("set", "set combo list", argsInfo: new string[] { "path=>combo file path" })]
        public void _SetComboList(string path)
        {
            SetComboList(path);
        }
    }
}
