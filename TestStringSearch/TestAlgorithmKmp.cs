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

        [Test]
        public void TestTinyCases()
        {
            PrefixCounterTestLogic.CompareTinyCounts(_kmp);
        }

        [Test]
        public void TestSimpleCase1()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_kmp, 1);
        }

        [Test]
        public void TestSimpleCase2()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_kmp, 2);
        }

        [Test]
        public void TestSimpleCase3()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_kmp, 3);
        }

        [Test]
        public void TestSmallCyclicCase()
        {
            PrefixCounterTestLogic.CompareSmallCyclicCounts(_kmp);
        }

        [Test]
        public void TestLargeCyclicCase()
        {
            PrefixCounterTestLogic.CompareLargeCyclicCounts(_kmp);
        }

        [Test]
        public void TestPerformance()
        {
            Profiling.TestAverageCountTime(_kmp);
        }
    }
}