using System;
using System.Collections.Generic;
using System.Net;

namespace RecordExecuter {
    public class Storage {
        public static List<object> Objects = new List<object> ();
        public static ThreadGuid Guid;
        public static List<string> ProxyList = new List<string> ();
        public static List<string> ComboList = new List<string> ();
        static bool load_proxy = false;
        static bool load_combo = false;
        public static int Success = 0;
        public static int Bad = 0;
        public static int GoodCombo=0;

        public static List<string> GoodProxy=new List<string>();

        public static void LoadProxy (string path) {
            if (!load_proxy) {
                ProxyList = Tools.ReadFile (path) as List<string>;
                load_proxy = true;
            }
        }
        public static void LoadCombo (string path) {
            if (!load_combo) {
                ComboList = Tools.ReadFile (path) as List<string>;
                load_combo = true;
            }
        }
    }
}