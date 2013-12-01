using System;
using NUnit.Framework;
using StringSearch;

namespace TestStringSearch
{
    [TestFixture]
    public class TestAlgorithmKmp
    {
        private AlgorithmKmp _kmp;

        [SetUp]
        public void Init()
        {
            _kmp = new AlgorithmKmp();
        }

        [Test] // A KMP specific test
        public void PartialMatchAtZeroIsZero()
        {
            var testCases = new[] {
                TestCases.GetSimpleCase(1),
                TestCases.GetSimpleCase(2),
                TestCases.GetSimpleCase(3),
                TestCases.SmallCyclicTestCase,
                TestCases.LargeCyclicTestCase
            };
            // KMP search implementation assumes pmt[0] == 0
            // Other variations may use -1 as an invalid value
            foreach (var s in testCases)
            {
                int[] count;
                var pmt1 = AlgorithmKmp.PartialMatchTable(s);
                var pmt2 = AlgorithmKmp.CountedPmt(s, out count);
                Assert.AreEqual(0, pmt1[0]);
                Assert.AreEqual(0, pmt2[0]);
            }
        }

        [Test]
        public void PrefixCountsAreValid()
        {
            _kmp.PrefixCountsShouldMatchExpected();
        }

        [Test]
        public void PrefixCountTimeIsAcceptable()
        {
            _kmp.PrefixCountTimeShouldBeAcceptable();
        }

        [Test]
        public void SearchSuccessReturnsMatchIndex()
        {
            _kmp.SearchShouldReturnMatchIndex();
        }

        [Test]
        public void SearchFailureReturnsN()
        {
            _kmp.SearchFailureShouldReturnN();
        }

        [Test]
        public void SearchAllReturnsAllMatchIndices()
        {
            _kmp.SearchAllShouldReturnAllMatches();
        }
    }
}