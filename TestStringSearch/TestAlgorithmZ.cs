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
            PrefixCounterTestLogic.TestTinyTextCounts(_z);
        }

        [Test]
        public void TestSimpleCase1()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_z, 1);
        }

        [Test]
        public void TestSimpleCase2()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_z, 2);
        }

        [Test]
        public void TestSimpleCase3()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_z, 3);
        }

        [Test]
        public void TestSmallCyclicCase()
        {
            PrefixCounterTestLogic.TestSmallCyclicTextCounts(_z);
        }

        [Test]
        public void TestLargeCyclicCase()
        {
            PrefixCounterTestLogic.TestLargeCyclicTextCounts(_z);
        }

        [Test]
        public void TestPerformance()
        {
            Profiling.TestAverageCountTime(_z);
        }
    }
}