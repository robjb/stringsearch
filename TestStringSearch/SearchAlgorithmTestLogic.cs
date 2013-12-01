using System;
using System.Linq;
using NUnit.Framework;
using StringSearch;

namespace TestStringSearch
{
    public static class SearchAlgorithmTestLogic
    {
        #region Helper Methods

        private static void TestSearchSubstr(ISearchAlgorithm<int[]> searcher, char[] s, int start, int substrLen)
        {
            if (start < 0)
            {
                throw new ArgumentException("start < 0");
            }
            if (substrLen < 1)
            {
                throw new ArgumentException("patternLen < 1");
            }
            // Select substring as pattern from a random index
            var pattern = s.Skip(start).Take(substrLen).ToArray();
            int result = searcher.Search(s, pattern);

            // Rather than comaring result with i, which would be a
            // bad test because i may not be the first occurence,
            // create match from result and compare with pattern.
            var match = s.Skip(result)
                .Take(pattern.Length)
                .ToArray();

            start = 0;
            Assert.AreEqual(pattern.Length, match.Length);
            foreach (char c in pattern)
            {
                Assert.AreEqual(c, match[start++]);
            }
        }

        private static void TestSearchSubstrLengths(ISearchAlgorithm<int[]> searcher, char[] s)
        {
            if (s.Length < 1)
            {
                throw new ArgumentException("s.Length < 1");
            }
            int n = s.Length;
            for (int i = 1; i < n; i++)
            {
                TestSearchSubstr(searcher, s, i, n - i);
            }
        }

        private static void TestSearchFailure(ISearchAlgorithm<int[]> searcher, char[] s)
        {
            if (s.Length < 1)
            {
                throw new ArgumentException("s.Length < 1");
            }
            int n = s.Length;

            // Build an impossible pattern by
            // modifying every character in s
            var pattern = s.Select(c => (char)(c + 1)).ToArray();
            Assert.AreEqual(n, searcher.Search(s, pattern));
        }

        private static void TestSearchMatches(ISearchAlgorithm<int[]> searcher, char[] s, char[] key, int expectedCount)
        {
            if (s.Length < 1)
            {
                throw new ArgumentException("s.Length < 1");
            }
            if (key.Length < 1)
            {
                throw new ArgumentException("key.Length < 1");
            }
            var keyLen = key.Length;
            var matchIndices = searcher.SearchAll(s, key);

            // Try to catch invalid indices early by
            // comparing result length with expected count
            Assert.AreEqual(expectedCount, matchIndices.Length);

            // For each match, compare every character to key
            foreach (var start in matchIndices)
            {
                var match = s.Skip(start).Take(keyLen).ToArray();

                // Try to catch invalid match without evaluating chars
                // by comparing match length against search key length
                Assert.AreEqual(keyLen, match.Length);
                for (int ci = 0; ci < keyLen; ci++)
                {
                    Assert.AreEqual(key[ci], match[ci]);
                }
            }
        }

        #endregion

        public static void SearchShouldReturnMatchIndex(this ISearchAlgorithm<int[]> searcher)
        {
            var testCases = new[]
            {
                TestCases.GetSimpleCase(1),
                TestCases.GetSimpleCase(2),
                TestCases.GetSimpleCase(3),
                TestCases.SmallCyclicTestCase
            };
            foreach (var s in testCases)
            {
                TestSearchSubstrLengths(searcher, s);
            }
        }

        public static void SearchFailureShouldReturnN(this ISearchAlgorithm<int[]> searcher)
        {
            var testCases = new[]
            {
                TestCases.GetSimpleCase(1),
                TestCases.GetSimpleCase(2),
                TestCases.GetSimpleCase(3),
                TestCases.SmallCyclicTestCase
            };
            foreach (var s in testCases)
            {
                TestSearchFailure(searcher, s);
            }
        }

        public static void SearchAllShouldReturnAllMatches(this ISearchAlgorithm<int[]> searcher)
        {
            // Only one test case is considered here
            // Fix this later to work on all of TestCases
            var testCases = new[] { "abababa" };
            var patterns = new[] { 
                new[] { "aba", "ab", "ba", "a", "b" }
            };
            var patternCounts = new[] {
                new[] {3,3,3,4,3}
            };
            // For each test case, search for multiple keys;
            // This O(n^2) loop should be safe, since only
            // small keys in small s values are evaluated.
            for (int i = 0; i < testCases.Length; i++)
            {
                var s = testCases[i].Chars();
                var keys = patterns[i];
                var expectedCount = patternCounts[i];
                for (int j = 0; j < keys.Length; j++)
                {
                    var key = keys[j].Chars();

                    // Here we create another possible O(n^2) loop,
                    // doing naive char comparison for every match.
                    // Still, keys are small, so this should be safe.
                    TestSearchMatches(searcher, s, key, expectedCount[j]);
                }
            }
        }
    }
}
