using System;
using System.Collections.Generic;
using System.Text;

namespace BCL.Utility
{
    public class ConfigAction
    {

		/// <summary>
		/// Add new header to default request headers
		/// </summary>
		/// <param name="headers">Http web request headers</param>
        protected void AddNewRequestDefaultsHeader(string headers)
        {
			try
			{
				var array = Utilities.GetArray(headers, Utilities.Mode_1);
				foreach(var header in array)
				{
					Utilities.DefaultRequestShowableHeaders[Utilities.DefaultRequestShowableHeaders.Length] = header;
				}
			}
			catch (Exception e)
			{
				CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
			}
		}


		/// <summary>
		/// Add new header to default response headers
		/// </summary>
		/// <param name="headers">Http web response headers</param>
		protected void AddNewResponseDefaultsHeader(string headers)
		{
			try
			{
				var array = Utilities.GetArray(headers,Utilities.Mode_1);
				foreach(var header in array)
				{
					Utilities.DefaultResponseShowableHeaders[Utilities.DefaultResponseShowableHeaders.Length] = header;
				}
			}
			catch (Exception e)
			{
				CMD.ShowApplicationMessageToUser($"message : {e.Message}\nroute : {this.ToString()}", showType: ShowType.DANGER);
			}
		}

		public void CutFirstOf(){
			//cut last of member from any list and return that 
        }

	}
}
