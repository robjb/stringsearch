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
        public void TestTinyCases()
        {
            PrefixCounterTestLogic.CompareTinyCounts(_z);
        }

        [Test]
        public void TestSimpleCase1()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_z, 1);
        }

        [Test]
        public void TestSimpleCase2()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_z, 2);
        }

        [Test]
        public void TestSimpleCase3()
        {
            PrefixCounterTestLogic.CompareSimpleCounts(_z, 3);
        }

        [Test]
        public void TestSmallCyclicCase()
        {
            PrefixCounterTestLogic.CompareSmallCyclicCounts(_z);
        }

        [Test]
        public void TestLargeCyclicCase()
        {
            PrefixCounterTestLogic.CompareLargeCyclicCounts(_z);
        }

        [Test]
        public void TestPerformance()
        {
            Profiling.TestAverageCountTime(_z);
        }
    }
}