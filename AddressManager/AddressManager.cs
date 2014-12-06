using System.Threading.Tasks;
using AddressManager.Interfaces;
using Models;
using Models.Address;
using ResourceAccess.Interfaces;

namespace AddressManager
{
    public class AddressManager :IAddressManager
    {
        private readonly IAddressResourceAccess _addressResourceAccess;

        public AddressManager(IAddressResourceAccess addressResourceAccess)
        {
            _addressResourceAccess = addressResourceAccess;
        }

        public async Task<ResourceResponse<bool>> SaveAddress(Address address)
        {
            var validatedAddress = ValidateAddressData(address);

            return (validatedAddress.IsSuccess()) ? 
                await _addressResourceAccess.SaveAddress(address) :
                validatedAddress;
        }

        public async Task<ResourceResponse<Address>> GetAddress(int addressId)
        {
            var validatedAddressId = ValidateAddressId(addressId);


            return (validatedAddressId.IsSuccess()) ?
                await _addressResourceAccess.GetAddress(addressId) :
                validatedAddressId;
        }

        private static ResourceResponse<Address> ValidateAddressId(int addressId)
        {
            var validatedAddressId = new ResourceResponse<Address>
            {
                Item = new Address(),
                StatusCode = ResourceResponseStatusCode.Ok,
                StatusDescription = "Valid Address Id"
            };

            // Address Id is int IDENTITY in the DB table. Proceed only if it appears valid
            if (addressId > 0 && addressId <= int.MaxValue && addressId is int)
                return validatedAddressId;

            validatedAddressId.Item = null;
            validatedAddressId.StatusCode = ResourceResponseStatusCode.InvalidRequest;
            validatedAddressId.StatusDescription = string.Format("Invalid Address ID submitted {0}", addressId);


            return validatedAddressId;
        }

        private static ResourceResponse<bool> ValidateAddressData(Address address)
        {
            var validatedAddress = new ResourceResponse<bool>
            {
                Item = true,
                StatusCode = ResourceResponseStatusCode.Ok,
                StatusDescription = "Valid Address Data"
            };

            // Address Id is int IDENTITY in the DB table. Proceed only if it appears valid
            if (address.AddressId < 0 || address.AddressId > int.MaxValue || address.AddressId.GetType() != typeof(int))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Invalid Address ID submitted {0}", address.AddressId);
            }

            // StateId is currently only accounting for US states and Canada Territories. Ensure StateId maps to existing FK ID
            if (address.State.Id < 1 || address.State.Id > 64 || address.State.Id.GetType() != typeof(int))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Invalid State (or Canadian Province) ID submitted {0}", address.State.Id);
            }

            // Ensure there is no string.Empty properties for the Not Null columns in the DB table
            if (string.IsNullOrEmpty(address.Name))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Empty Name submitted {0}", address.Name);
            }
            if (string.IsNullOrEmpty(address.AddressLine1))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Empty AddressLine1 submitted {0}", address.AddressLine1);
            }
            if (string.IsNullOrEmpty(address.City))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Empty City submitted {0}", address.City);
            }
            if (string.IsNullOrEmpty(address.Zip))
            {
                validatedAddress.Item = false;
                validatedAddress.StatusCode = ResourceResponseStatusCode.InvalidRequest;
                validatedAddress.StatusDescription = string.Format("Empty Zip submitted {0}", address.Zip);
            }


            return validatedAddress;
        }
    }
}
    