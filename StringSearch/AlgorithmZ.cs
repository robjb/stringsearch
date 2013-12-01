using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StringSearch
{
    // Algorithm Z will have computed an int[] after completing, 
    // then requires the int[] computed by Z to count prefixes.
    // Also implements IPrefixCounter<char[]> if users wish to 
    // count prefixes directly from s (rather than z.)
    public class AlgorithmZ : ISearchAlgorithm<int[]>, IPrefixCounter<int[]>, IPrefixCounter<char[]>
    {
        private static T[] Concat<T>(T[] t1, T[] t2)
        {
            int n1 = t1.Length;
            int n2 = t2.Length;
            var s = new T[n1 + n2];

            Array.Copy(t1, s, n1);
            Array.Copy(t2, 0, s, n1, n2);

            return s;
        }

        #region IPrefixCounter

        public int[] CountPrefixes(int[] z)
        {
            return CountPrefixLengths(z);
        }

        public int[] CountPrefixes(char[] s)
        {
            return CountPrefixLengths(Z(s));
        }

        #endregion

        #region ISearchAlgorithm

        public string Name
        {
            get { return "Z-Algorithm"; }
        }

        public int[] PreProcess(char[] s)
        {
            return Z(s);
        }

        public int Search(char[] s, char[] key)
        {
            int n = s.Length;
            int m = key.Length;

            // Build s2 by prepending key to s
            var s2 = Concat(key, s);

            // Searching Z(s2) for prefix length m
            // should yield occurences of key in s2
            var z = Z(s2);
            for (int i = m; i < z.Length; i++)
            {
                if (z[i] >= m)
                {
                    // subtract key length m to index s from Z(s2)
                    return i - m; 
                }
            }
            return n; // no matches
        }

        public int[] SearchAll(char[] s, char[] key)
        {
            int m = key.Length;

            // Build s2 by prepending key to s
            var s2 = Concat(key, s);

            // Searching Z(s2) for prefix length m
            // should yield occurences of key in s2
            var z = Z(s2);

            var startIndices = new List<int>();
            for (int i = m; i < z.Length; i++)
            {
                if (z[i] >= m)
                {
                    // subtract key length m to index s from Z(s2)
                    startIndices.Add(i - m);
                }
            }
            return startIndices.ToArray();
        }

        #endregion

        #region Implementation

        private static int[] CountPrefixLengths(int[] z)
        {
            int n = z.Length;
            var prefixCount = new int[n + 1]; // indexed by length
            Array.Clear(prefixCount, 0, n + 1);

            // Each z[i] is the length of longest common prefix between S and S[i..n]
            // Since S[i..n] matches prefix of length z[i], increment count for each z[i].
            for (int i = 0; i < n; i++)
            {
                // Count the longest prefix-substr occurence; shorter occurences counted below.
                prefixCount[z[i]]++;
            }
            // In descending order: if prefix S[0..i] occurs N times,
            // the sub-prefix S[0 .. i-1] must also occur >= N times.
            for (int i = n - 1; i > 0; i--)
            {
                prefixCount[i - 1] += prefixCount[i];
                prefixCount[i]++; // Count prefix itself
            }
            prefixCount[0] = 0;   // Clear incorrect count for the empty prefix
            prefixCount[n] = 1;   // We haven't yet counted the complete string
            return prefixCount;
        }

        private static int[] Z(char[] s)
        {
            int n = s.Length;
            var z = new int[n];
            int left = 0, right = 0;

            // Used to advance right edge of substring interval until
            // S[left .. right] represents the longest proper prefix
            // substring such that 1 <= left <= i <= right. This also
            // produces z[i], length of the longest substring in S 
            // starting at position i, which is also a prefix of S.
            Action<int> scanWhileEq = i =>
            {
                // Scans along S, advancing right edge of the
                // substring interval until an inequality is found
                while (right < n && s[right - left] == s[right]) { right++; }

                z[i] = right - left; // algorithm uses R-L+1, but R has been incremented above;
                right--;			 // we just fall back to correct value after setting z[i]
            };
            // We consider substrings S[i] of S starting at i = 1,
            // where i indicates the start index of the substring
            for (int i = 1; i < n; i++)
            {
                // No prefix exists in the substring interval
                // Now we start a new interval at i and advance
                // right edge until an inequality (or n) is hit.
                if (i > right)
                {
                    left = right = i;
                    scanWhileEq(i);
                }
                else
                {   // The current S[L..R] match against S[0] contains i;
                    // Let k = i-L, then S[i] matches S[k] for at least
                    // R-i+1 chars, so z[i] bust be >= min(z[k], R-i+1)
                    int k = i - left;

                    // If longest prefix-substring match from S[k] is
                    // less than the length of the match against S[i],
                    // the match between S[i] and S[k] is not a prefix
                    if (z[k] < (right - i + 1))
                    {
                        // There is no longer prefix-substring at S[i]
                        // than the z[k] length prefix match at S[k]
                        z[i] = z[k];
                    }
                    else
                    {   // S[i] can match S[0] for more than R-i+1 chars
                        // e.g. the match could extend past right edge;
                        // reset left edge & scan right to update z[i]
                        left = i;
                        scanWhileEq(i);
                    }
                }
            }
            return z;
        }

        #endregion
    }
}
