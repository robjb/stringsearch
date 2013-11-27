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
            PrefixCounterTestLogic.TestTinyTextCounts(_kmp);
        }

        [Test]
        public void TestSimpleCase1()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_kmp, 1);
        }

        [Test]
        public void TestSimpleCase2()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_kmp, 2);
        }

        [Test]
        public void TestSimpleCase3()
        {
            PrefixCounterTestLogic.TestSimpleTextCounts(_kmp, 3);
        }

        [Test]
        public void TestSmallCyclicCase()
        {
            PrefixCounterTestLogic.TestSmallCyclicTextCounts(_kmp);
        }

        [Test]
        public void TestLargeCyclicCase()
        {
            PrefixCounterTestLogic.TestLargeCyclicTextCounts(_kmp);
        }

        [Test]
        public void TestPerformance()
        {
            Profiling.TestAverageCountTime(_kmp);
        }
    }
}