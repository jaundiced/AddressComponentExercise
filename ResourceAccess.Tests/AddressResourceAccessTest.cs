using DataAccess.Interfaces;
using Models;
using Models.Address;
using Moq;
using NUnit.Framework;
using ResourceAccess.Interfaces;

namespace ResourceAccess.Tests
{
    public class AddressResourceAccessTest
    {
        private IAddressResourceAccess _instanceToTest;
        private readonly Mock<IAddress> _mockDataProvider = new Mock<IAddress>();
        private  Address _testAddress;

        [TestFixtureSetUp]
        public void Setup()
        {
            _testAddress = new Address
            {
                AddressId = 10,
                AddressLine1 = "1234 Main Street",
                AddressLine2 = "Suite 100",
                Company = "Some Company",
                State = new State { Id = 48, Abbreviation = "WA", Name = "Washington" },
                City = "Seattle",
                Zip = "98101",
                Name = "John Doe"
            };
        }


        [Category("AddressResourceAccessTest")]
        [Test(Description = "Should save new or update existing address to the DB, return true")]
        public async void Should_Save_Or_Update_Address_And_Return_True()
        {
            //Arrange
            _mockDataProvider.Setup(a => a.SaveAddress(It.IsAny<Address>()))
                .Returns(true);

            _instanceToTest = new AddressResourceAccess(_mockDataProvider.Object);
            

            //Act
            var result = await _instanceToTest.SaveAddress(_testAddress);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.Ok);
            Assert.NotNull(result.Item);
            Assert.IsTrue(result.Item);
        }

        [Category("AddressResourceAccessTest")]
        [Test(Description = "Should return false when saving/updating address to the DB")]
        public async void Should_Return_False_Upon_Failed_Save_Or_Update_Address()
        {
            //Arrange
            _mockDataProvider.Setup(a => a.SaveAddress(It.IsAny<Address>()))
                .Returns(false);

            _instanceToTest = new AddressResourceAccess(_mockDataProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(_testAddress);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.NotFound);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }


        [Category("AddressResourceAccessTest")]
        [Test(Description = "Should return Address for valid Address Id")]
        public async void Should_Return_Address_For_Valid_Id()
        {
            //Arrange

            _mockDataProvider.Setup(a => a.TryGetAddress(It.IsAny<int>(), out _testAddress))
                .Returns(true);

            _instanceToTest = new AddressResourceAccess(_mockDataProvider.Object);

            //Act
            var result = await _instanceToTest.GetAddress(_testAddress.AddressId);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.Ok);
            Assert.NotNull(result.Item);
            Assert.That(result.Item.AddressId == _testAddress.AddressId);
            Assert.That(result.Item.AddressLine1 == _testAddress.AddressLine1);
            Assert.That(result.Item.AddressLine2 == _testAddress.AddressLine2);
            Assert.That(result.Item.City == _testAddress.City);
            Assert.That(result.Item.Company == _testAddress.Company);
            Assert.That(result.Item.Name == _testAddress.Name);
            Assert.That(result.Item.Zip == _testAddress.Zip);
            Assert.That(result.Item.State.Id == _testAddress.State.Id);
        }

        [Category("AddressResourceAccessTest")]
        [Test(Description = "Should return Address for invalid Address Id")]
        public async void Should_Return_Expected_Response_Code_For_Invalid_Id()
        {
            //Arrange
            _testAddress.AddressId = -1;
            _mockDataProvider.Setup(a => a.TryGetAddress(It.IsAny<int>(), out _testAddress))
                .Returns(false);

            _instanceToTest = new AddressResourceAccess(_mockDataProvider.Object);

            //Act
            var result = await _instanceToTest.GetAddress(_testAddress.AddressId);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.NotFound);
        }
    }
}
