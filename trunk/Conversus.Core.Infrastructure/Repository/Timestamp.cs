using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conversus.Core.Infrastructure
{
    public static class TimestampFactory
    {
        private static long _timestamp = 0;

        public static long GetCurrentTimestamp()
        {
            return System.Threading.Interlocked.Read(ref _timestamp);
        }

        internal static long GetNewTimestamp()
        {
            return System.Threading.Interlocked.Increment(ref _timestamp);
        }
    }
}
