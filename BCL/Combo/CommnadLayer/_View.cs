using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Combo
{
    [ClassCommandInfo("combo_view")]
    class _View:ViewAction
    {
        [ClassCommandInfo("list","display combo list")]
        public void _DisplayComboList()
        {
            DisplayComboList();
        }
    }
}
