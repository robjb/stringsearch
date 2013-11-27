using System;
using System.Diagnostics;
using NUnit.Framework;
using StringSearch;

namespace TestStringSearch
{
    public static class Profiling
    {
        public static double TimePrefixCounting(IPrefixCounter<char[]> counter, char[] s, out int[] count)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            count = counter.CountPrefixes(s);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalSeconds;
        }

        public static double TimeAverageCount(IPrefixCounter<char[]> counter, int runs, int textLen)
        {
            if (runs < 100)
            {
                throw new ArgumentOutOfRangeException("runs < 100 will produce unreliable results.");
            }
            if (textLen < 1)
            {
                throw new ArgumentOutOfRangeException("textLen < 1");
            }
            double seconds = 0.0;
            foreach (var s in TestCases.RandomCharArrays(runs, textLen))
            {
                int[] count;
                seconds += TimePrefixCounting(counter, s, out count);
            }
            return seconds / runs;
        }

        public static void TestAverageCountTime(IPrefixCounter<char[]> counter)
        {
            const double maxAvg = 0.1;
            const double macheps = Double.Epsilon;
            
            var avgTime = TimeAverageCount(counter, 300, 300000);
            Assert.IsTrue((maxAvg - avgTime) >= macheps);
        }
    }
}
