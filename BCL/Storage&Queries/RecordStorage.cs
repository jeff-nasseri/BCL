using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BCL
{
    public class RecordStorage
    {
        public static IDictionary<int, (object modelInstance, MethodInfo method, object[] args)> Records =
            new Dictionary<int, (object modelInstance, MethodInfo method, object[] args)>();
        protected static bool RecordState = true;

    }
}
