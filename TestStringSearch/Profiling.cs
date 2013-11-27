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

        // I realize this is a silly unit test, and performance will vary
        // by hardware. However, both algorithms should be able to pass
        // this test easily on modest hardware.
        public static void TestAverageCountTime(IPrefixCounter<char[]> counter)
        {
            const int runs = 300;       // Keep runs small so unit tests don't run long
            const int chars = 300000;   // Either algorithm should be able to process 300k
            const double maxAvg = 0.1;  // characters in a 10th of a second easily.
            const double macheps = Double.Epsilon;
            
            var avgTime = TimeAverageCount(counter, runs, chars);
            Assert.IsTrue((maxAvg - avgTime) >= macheps);
        }
    }
}
