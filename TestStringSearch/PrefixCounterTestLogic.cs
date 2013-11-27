using NUnit.Framework;
using StringSearch;
using System;
using System.Linq;

namespace TestStringSearch
{
    public static class PrefixCounterTestLogic
    {
        public static void CompareTestCaseCounts(IPrefixCounter<char[]> counter, char[] s, int[] expectedCounts)
        {
            var n = s.Length;
            if (expectedCounts.Length != n + 1)
            {
                throw new ArgumentOutOfRangeException("expectedCounts.Length != s.Length + 1");
            }
            var prefixCounts = counter.CountPrefixes(s);
            Assert.AreEqual(n + 1, prefixCounts.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(expectedCounts[i], prefixCounts[i]);
            }
        }

        public static void CompareTinyCounts(IPrefixCounter<char[]> counter)
        {
            const string s1 = "a";
            const string s2 = "ab";
            const string s3 = "aa";
            var expectedCounts1 = new[] { 0, 1 };
            var expectedCounts2 = new[] { 0, 1, 1 };
            var expectedCounts3 = new[] { 0, 2, 1 };

            Func<string, char[]> chars = s => s.ToCharArray();
            CompareTestCaseCounts(counter, chars(s1), expectedCounts1);
            CompareTestCaseCounts(counter, chars(s2), expectedCounts2);
            CompareTestCaseCounts(counter, chars(s3), expectedCounts3);
        }

        public static void CompareSimpleCounts(IPrefixCounter<char[]> counter, int testCaseId)
        {
            int[] expectedCount;
            var simpleText = TestCases.GetSimpleCase(testCaseId, out expectedCount);
            CompareTestCaseCounts(counter, simpleText, expectedCount);
        }

        private static void CompareCyclicCounts(IPrefixCounter<char[]> counter, int[] expected, int repetitions)
        {
            var first = new string(TestCases.SimpleTestCases.First());
            var cyclicText = TestCases.CreateCyclicTestCase(first, repetitions);

            CompareTestCaseCounts(counter, cyclicText, expected);
        }

        public static void CompareSmallCyclicCounts(IPrefixCounter<char[]> counter)
        {
            var expectedCounts = TestCases.SmallCyclicExpectedCounts;
            CompareCyclicCounts(counter, expectedCounts, 16);
        }

        public static void CompareLargeCyclicCounts(IPrefixCounter<char[]> counter)
        {
            var expectedCounts = TestCases.LargeCyclicExpectedCounts;
            CompareCyclicCounts(counter, expectedCounts, 256);
        }
    }
}