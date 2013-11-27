using NUnit.Framework;
using StringSearch;
using System;
using System.Linq;

namespace TestStringSearch
{
    public static class PrefixCounterTestLogic
    {
        public static void TestTinyTextCounts(IPrefixCounter<char[]> counter)
        {
            const string s1 = "a";
            const string s2 = "ab";
            const string s3 = "aa";
            var expectedCounts1 = new[] { 0, 1 };
            var expectedCounts2 = new[] { 0, 1, 1 };
            var expectedCounts3 = new[] { 0, 2, 1 };

            Func<string, char[]> chars = s => s.ToCharArray();
            TestSimpleTextCounts(counter, chars(s1), expectedCounts1);
            TestSimpleTextCounts(counter, chars(s2), expectedCounts2);
            TestSimpleTextCounts(counter, chars(s3), expectedCounts3);
        }

        public static void TestSimpleTextCounts(IPrefixCounter<char[]> counter, char[] s, int[] expectedCounts)
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

        public static void TestSimpleTextCounts(IPrefixCounter<char[]> counter, int testCaseId)
        {
            int[] expectedCount;
            var simpleText = TestCases.GetSimpleCase(testCaseId, out expectedCount);
            TestSimpleTextCounts(counter, simpleText, expectedCount);
        }

        // An off-by-one error had caused KMP fail this test initially,
        // but only after repetitions was increased to at least 16.
        private static void TestCyclicTextCounts(IPrefixCounter<char[]> counter, int[] expected, int repetitions)
        {
            var first = new string(TestCases.SimpleTestCases.First());
            var cyclicText = TestCases.CreateCyclicTestCase(first, repetitions);
            TestSimpleTextCounts(counter, cyclicText, expected);
        }

        public static void TestSmallCyclicTextCounts(IPrefixCounter<char[]> counter)
        {
            // These counts were pre-computed by Z and verified with KMP for
            // s = TestCases.CreateCyclicTestCase(SimpleTestCases.First(), 16)
            //
            // They can be trusted as reliable because these algorithms
            // were also used to pass a competitive programming problem.
            var expectedCounts = TestCases.SmallCyclicExpectedCounts;
            TestCyclicTextCounts(counter, expectedCounts, 16);
        }

        public static void TestLargeCyclicTextCounts(IPrefixCounter<char[]> counter)
        {
            // These counts were pre-computed by Z and verified with KMP for
            // s = TestCases.CreateCyclicTestCase(SimpleTestCases.First(), 256)
            //
            // They can be trusted as reliable because these algorithms
            // were also used to pass a competitive programming problem.
            var expectedCounts = TestCases.LargeCyclicExpectedCounts;
            TestCyclicTextCounts(counter, expectedCounts, 256);
        }
    }
}