using System.Threading.Tasks;
using AddressManager.Interfaces;
using Models;
using Models.Address;
using Moq;
using NUnit.Framework;
using ResourceAccess.Interfaces;

namespace AddressManager.Tests
{
    public class AddressManagerTest
    {
        private IAddressManager _instanceToTest;
        private readonly Mock<IAddressResourceAccess> _mockResourceAccessProvider = new Mock<IAddressResourceAccess>();
            
        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should save new Address")]
        public async void Should_Save_New_Address()
        {
            //Arrange
            _mockResourceAccessProvider.Setup(a => a.SaveAddress(It.IsAny<Address>()))
                .Returns(Task.FromResult(new ResourceResponse<bool>{Item = true}));

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(GetTestAddress());

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.Ok);
            Assert.NotNull(result.Item);
            Assert.IsTrue(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should update  existing Address with data populated as expected")]
        public async void Should_Update_Valid_Existing_Address()
        {
            //Arrange
            _mockResourceAccessProvider.Setup(a => a.SaveAddress(It.IsAny<Address>()))
                .Returns(Task.FromResult(new ResourceResponse<bool> { Item = true }));

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(GetTestAddress());

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.Ok);
            Assert.NotNull(result.Item);
            Assert.IsTrue(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to save new Address with empty Name property")]
        public async void Should_Fail_To_Save_New_Address_With_Empty_Name()
        {
            //Arrange
            var address = GetTestAddress();
            address.AddressId = 0;
            address.Name = string.Empty;
            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }


        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with invalid Address Id")]
        public async void Should_Fail_To_Update_Address_With_Invalid_Address_Id()
        {
            //Arrange
            var address = GetTestAddress();
            address.AddressId = -1;
            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with invalid State Id exceeding upper bound")]
        public async void Should_Fail_To_Update_Address_With_Invalid_UpperBound_State_Id()
        {
            //Arrange
            var address = GetTestAddress();
            address.State.Id = 150;
            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with invalid State Id of Zero")]
        public async void Should_Fail_To_Update_Address_With_State_Id_Of_Zero()
        {
            //Arrange
            var address = GetTestAddress();
            address.State.Id = 0;

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with empty Name")]
        public async void Should_Fail_To_Update_Address_With_Empty_Name()
        {
            //Arrange
            var address = GetTestAddress();
            address.Name = string.Empty;

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with empty AddressLine1")]
        public async void Should_Fail_To_Update_Address_With_Empty_Address_Line_One()
        {
            //Arrange
            var address = GetTestAddress();
            address.AddressLine1 = string.Empty;

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with empty City")]
        public async void Should_Fail_To_Update_Address_With_Empty_City()
        {
            //Arrange
            var address = GetTestAddress();
            address.City = string.Empty;

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to update existing Address with empty Zip")]
        public async void Should_Fail_To_Update_Address_With_Empty_Zip()
        {
            //Arrange
            var address = GetTestAddress();
            address.Zip = string.Empty;

            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.SaveAddress(address);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.NotNull(result.Item);
            Assert.IsFalse(result.Item);
        }

        [Category("AddressManagerTest")]
        [Test(Description = "Should retrieve existing Address with valid AddressId")]
        public async void Should_Retrieve_Address_By_Valid_AddressId()
        {
            //Arrange
            var address = GetTestAddress();
            address.AddressId = 1;
            _mockResourceAccessProvider.Setup(a => a.GetAddress(It.IsAny<int>()))
                .Returns(Task.FromResult(new ResourceResponse<Address>
                {
                    Item = address, 
                    StatusCode = ResourceResponseStatusCode.Ok
                }));


            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.GetAddress(1);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.Ok);
            Assert.NotNull(result.Item);
            Assert.That(result.Item.AddressId == 1);
            Assert.That(result.Item.State.Id == address.State.Id);
            Assert.That(result.Item.Name == address.Name);
            Assert.That(result.Item.City == address.City);
            Assert.That(result.Item.AddressLine1 == address.AddressLine1);
            Assert.That(result.Item.Zip == address.Zip);
        }


        [Category("AddressManagerTest")]
        [Test(Description = "Should fail to retrieve Address with ID of zero")]
        public async void Should_Fail_To_Retrieve_Address_With_Id_Of_Zero()
        {
            //Arrange
            _instanceToTest = new AddressManager(_mockResourceAccessProvider.Object);

            //Act
            var result = await _instanceToTest.GetAddress(0);

            //Assert
            Assert.That(result.StatusCode == ResourceResponseStatusCode.InvalidRequest);
            Assert.IsNull(result.Item);
        }




        // Helper methods

        private static Address GetTestAddress()
        {
            return new Address
            {
                AddressId = 0,
                AddressLine1 = "1234 Main Street",
                AddressLine2 = "Suite 100",
                Company = "Some Company",
                State =new State{Id = 48, Abbreviation = "WA", Name = "Washington"},
                City = "Seattle",
                Zip = "98101",
                Name = "John Doe"
            };
        }

    }
}
