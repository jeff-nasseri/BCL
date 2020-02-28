using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Combo
{
    class ViewAction
    {

		/// <summary>
		/// display combo list
		/// </summary>
        public void DisplayComboList()
        {
			try
			{
				var comboList = LooperQueries.GetComboList();
				var count = 0;
				foreach(var combo in comboList)
				{
					CMD.ShowApplicationMessageToUser($"{++count} ) {combo}");
				}
			}
			catch (Exception e)
			{
				CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
			}
		}
    }
}
