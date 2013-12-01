using System;
using NUnit.Framework;
using StringSearch;

namespace TestStringSearch
{
    [TestFixture]
    public class TestAlgorithmZ
    {
        private AlgorithmZ _z;

        [SetUp]
        public void Init()
        {
            _z = new AlgorithmZ();
        }

        [Test]
        public void PrefixCountsAreValid()
        {
            _z.PrefixCountsShouldMatchExpected();
        }

        [Test]
        public void PrefixCountTimeIsAcceptable()
        {
            _z.PrefixCountTimeShouldBeAcceptable();
        }

        [Test]
        public void SearchSuccessReturnsMatchIndex()
        {
            _z.SearchShouldReturnMatchIndex();
        }

        [Test]
        public void SearchFailureReturnsN()
        {
            _z.SearchFailureShouldReturnN();
        }

        [Test]
        public void SearchAllReturnsAllMatchIndices()
        {
            _z.SearchAllShouldReturnAllMatches();
        }
    }
}