using Moq;
using NUnit.Framework;

namespace Education.Tests.Helpers
{
    [TestFixture]
    public class DataHelperTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void SetUp() {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private DataHelper CreateDataHelper() {
            return new DataHelper();
        }

        //[Test]
        //public void GetName_StateUnderTest_ExpectedBehavior() {
        //    var dataHelper = this.CreateDataHelper();

        //    var result = dataHelper.GetName();

        //    Assert.AreEqual(result, "zheka");
        //}

        [Test]
        public void GetStatusMessage_StateUnderTest_ExpectedBehavior() {
            var dataHelper = this.CreateDataHelper();
            bool status = false;

            var result = dataHelper.GetStatusMessage(status);

            Assert.AreEqual(result, "Not ok");
        }

        [Test]
        public void GetStatusMessage_StateUnderTest_ExpectedBehavior1() {
            var dataHelper = this.CreateDataHelper();
            bool status = true;

            var result = dataHelper.GetStatusMessage(status);

            Assert.AreEqual(result, "Its ok");
        }
    }
}
