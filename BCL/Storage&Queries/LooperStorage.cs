using System;
using System.Collections.Generic;
using System.Text;

namespace BCL
{
    public class LooperQueries : LooperStorage
    {

        /// <summary>
        /// return list of proxies
        /// </summary>
        public static List<string> GetProxies()
        {
            return Proxies;
        }

        /// <summary>
        /// set proxies in proxy list from file
        /// </summary>
        /// <param name="path">file path</param>
        public static void SetProxies(string path)
        {
            Proxies = Utilities.ReadFile(path) as List<string>;
        }

        /// <summary>
        /// remove proxy from proxies
        /// </summary>
        public static void RemoveProxy(string proxy)
        {
            Proxies.Remove(proxy);
        }

        /// <summary>
        /// write proxy list to file
        /// </summary>
        /// <param name="path">file path</param>
        public static void WriteProxies(string path)
        {
            Utilities.WriteFile<string>(path, Proxies);
        }

        /// <summary>
        /// remove all null value from proxies list
        /// </summary>
        public static void RemoveAllNullValue()
        {
            Proxies.RemoveAll(_ =>
            {
                if (_ == null)
                    return true;
                return false;
            });
        }

        /// <summary>
        /// return list of combo
        /// </summary>
        public static List<string> GetComboList()
        {
            return ComboList;
        }

        /// <summary>
        /// set combo list
        /// </summary>
        /// <param name="comboList">list of combo</param>
        public static void SetComboList(List<string> comboList)
        {
            ComboList = comboList;
        }
    }
}
