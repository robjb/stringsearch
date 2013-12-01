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

        public static double TimeAverageCount(IPrefixCounter<char[]> counter, int runs, int chars)
        {
            if (runs < 100)
            {
                throw new ArgumentOutOfRangeException("runs < 100 will produce unreliable results.");
            }
            if (chars < 1)
            {
                throw new ArgumentOutOfRangeException("chars < 1");
            }
            double seconds = 0.0;
            foreach (var s in TestCases.RandomCharArrays(runs, chars))
            {
                int[] count;
                seconds += TimePrefixCounting(counter, s, out count);
            }
            return seconds / runs;
        }

        // I realize this is a silly unit test, and performance will vary
        // by hardware. However, both algorithms should be able to pass
        // this test easily on modest hardware.
        public static void TestAverageCountTime(IPrefixCounter<char[]> counter)
        {
            const int runs = 100; // Keep runs small so unit tests don't run long
            const int chars = 300000;   // Either algorithm hould be able to process
            const double maxAvg = 0.1;  // 300k characters in a 10th of a second easily.
            const double macheps = Double.Epsilon;
            
            var avgTime = TimeAverageCount(counter, runs, chars);
            Assert.IsTrue((maxAvg - avgTime) >= macheps);
        }
    }
}
