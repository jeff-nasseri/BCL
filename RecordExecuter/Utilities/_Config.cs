using System.Collections.Generic;
using System.Linq;

namespace RecordExecuter.Utility {
    public class _Config {
        public string _CutFirstOf (List<string> list) {
            var first = list.First ();
            list.Remove (first);
            return first;
        }
    }
}