using NUnit.Framework;
using StringSearch;
using System;

namespace TestStringSearch
{
    public static class PrefixCounterTestLogic
    {
        #region Helper Methods

        private static void TestTinyTextCounts(IPrefixCounter<char[]> counter)
        {
            const string s1 = "a";
            const string s2 = "ab";
            const string s3 = "aa";
            var expectedCounts1 = new[] { 0, 1 };
            var expectedCounts2 = new[] { 0, 1, 1 };
            var expectedCounts3 = new[] { 0, 2, 1 };

            TestSimpleTextCounts(counter, s1.Chars(), expectedCounts1);
            TestSimpleTextCounts(counter, s2.Chars(), expectedCounts2);
            TestSimpleTextCounts(counter, s3.Chars(), expectedCounts3);
        }

        private static void TestSimpleTextCounts(IPrefixCounter<char[]> counter, char[] s, int[] expectedCounts)
        {
            var n = s.Length;
            if (expectedCounts.Length != n + 1)
            {
                throw new ArgumentException("expectedCounts.Length != s.Length + 1");
            }
            var prefixCounts = counter.CountPrefixes(s);
            Assert.AreEqual(n + 1, prefixCounts.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(expectedCounts[i], prefixCounts[i]);
            }
        }

        private static void TestSimpleTextCounts(IPrefixCounter<char[]> counter, int testCaseId)
        {
            int[] expectedCount;
            var s = TestCases.GetSimpleCase(testCaseId, out expectedCount);

            TestSimpleTextCounts(counter, s, expectedCount);
        }

        private static void TestSmallCyclicTextCounts(IPrefixCounter<char[]> counter)
        {
            // These counts were pre-computed by Z and verified 
            // with KMP for s = TestCases.SmallCyclicTestCase
            //
            // They can be trusted as reliable because these algorithms
            // were also used to pass a competitive programming problem.
            var s = TestCases.SmallCyclicTestCase;
            var expected = TestCases.SmallCyclicExpectedCounts;
            TestSimpleTextCounts(counter, s, expected);
        }

        private static void TestLargeCyclicTextCounts(IPrefixCounter<char[]> counter)
        {
            // These counts were pre-computed by Z and verified 
            // with KMP for s = TestCases.LargeCyclicTestCase
            //
            // They can be trusted as reliable because these algorithms
            // were also used to pass a competitive programming problem.
            var s = TestCases.LargeCyclicTestCase;
            var expected = TestCases.LargeCyclicExpectedCounts;
            TestSimpleTextCounts(counter, s, expected);
        }

        #endregion

        public static void PrefixCountsShouldMatchExpected(this IPrefixCounter<char[]> counter)
        {
            TestTinyTextCounts(counter);
            TestSimpleTextCounts(counter, 1);
            TestSimpleTextCounts(counter, 2);
            TestSimpleTextCounts(counter, 3); 
            TestSmallCyclicTextCounts(counter);
            TestLargeCyclicTextCounts(counter);
        }

        public static void PrefixCountTimeShouldBeAcceptable(this IPrefixCounter<char[]> counter)
        {
            Profiling.TestAverageCountTime(counter);
        }
    }
}