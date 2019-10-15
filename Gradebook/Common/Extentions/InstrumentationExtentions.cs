using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public static class InstrumentationExtenstions
    {
        private static Dictionary<Guid, Stopwatch> _StopWatches = new Dictionary<Guid, Stopwatch>();
        public static double GetPreciseElapsedTime(this Instrumentation instrumentation)
        {
            var fieldInfo = typeof(Instrumentation).GetField("_startedAt", BindingFlags.Instance | BindingFlags.NonPublic);
            var startedAt = (DateTime)fieldInfo.GetValue(instrumentation);
            return new TimeSpan(DateTime.Now.Ticks - startedAt.Ticks).TotalSeconds;
        }

        public static void StartWithPrecision (this Instrumentation instrumentation)
        {
            Contract.Requires(instrumentation != null);
            _StopWatches[instrumentation.Id] = Stopwatch.StartNew();
        }

        public static long GetReallyPreciseElapsedTime(this Instrumentation instrumentation)
        {
            Contract.Requires(instrumentation != null);
            return _StopWatches[instrumentation.Id].ElapsedMilliseconds;
        }
    }
}
