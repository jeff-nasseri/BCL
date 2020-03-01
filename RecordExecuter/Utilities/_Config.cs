using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RecordExecuter.Utility {
    public class _Config {
        public string _CutFirstOf (List<string> list) {
            var first = list.First ();
            list.RemoveAt (list.IndexOf(first));
            if(list.Count()<100){
                System.Console.WriteLine();
            }
            return first;
        }
    }
}