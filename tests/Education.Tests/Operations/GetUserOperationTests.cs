using Education.Tests.Operations;
using Moq;
using NUnit.Framework;

namespace Education.Tests.Operations
{
    [TestFixture]
    public class GetUserOperationTests
    {
        private GetUserOperation getUserOperation;
        private MockRepository mockRepository;
        private Mock<IGetPersonNameOperation> getPersonNameOperationMock;
        private Mock<IGetPersonSurnameOperation> getPersonSurnameOperationMock;

        [SetUp]
        public void Setup() {
            SetupData();
            SetupMocks();
        }

        [Test]
        public void GetUserData_When_StateUnderTest_Should_ExpectedBehavior() {

        }

        [Test]
        public void GetName_When_StateUnderTest_Should_ExpectedBehavior() {

        }

        [Test]
        public void GetSurname_When_StateUnderTest_Should_ExpectedBehavior() {

        }

        private GetUserOperation BuildGetUserOperation() =>
            new GetUserOperation(
                getPersonNameOperationMock.Object,
                getPersonSurnameOperationMock.Object);

        private void SetupData() {
            mockRepository = new MockRepository(MockBehavior.Default);
            getPersonNameOperationMock = mockRepository.Create<IGetPersonNameOperation>();
            getPersonSurnameOperationMock = mockRepository.Create<IGetPersonSurnameOperation>();
            getUserOperation = BuildGetUserOperation();
        }

        private void SetupMocks() {
        }
    }
}
