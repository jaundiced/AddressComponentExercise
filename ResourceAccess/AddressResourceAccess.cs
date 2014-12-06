using System.Threading.Tasks;
using Models;
using Models.Address;
using ResourceAccess.Interfaces;
using DataAccess.Interfaces;
using System;

namespace ResourceAccess
{
    public class AddressResourceAccess: IAddressResourceAccess
    {
        private readonly IAddress _addressData;

        public AddressResourceAccess(IAddress addressData)
        {
            _addressData = addressData;
        }

        public async Task<ResourceResponse<bool>> SaveAddress(Address address)
        {

            var businessModel = new ResourceResponse<bool>();

            await Task.Run(() =>
            {
                try
                {
                    var dataModel = _addressData.SaveAddress(address);
                    if (dataModel == false)
                    {
                        businessModel.StatusCode = ResourceResponseStatusCode.NotFound;
                    }
                    else
                    {
                        businessModel.StatusCode = ResourceResponseStatusCode.Ok;
                        businessModel.Item = true;
                    }

                }
                catch (Exception ex)
                {
                    businessModel.StatusCode = ResourceResponseStatusCode.ServerError;
                    businessModel.Item = false;

                    Console.WriteLine("AddressResourceAccess SaveAddress() error:" + ex.Message);
                    // Log to applicable logging repository here
                }
            });

            return businessModel;
        }

        public async Task<ResourceResponse<Address>> GetAddress(int id)
        {

            var businessModel = new ResourceResponse<Address>();
            var address = new Address();

            await Task.Run(() =>
            {
                try
                {
                    var dataModel = _addressData.TryGetAddress(id, out address);
                    if (dataModel == false)
                    {
                        businessModel.StatusCode = ResourceResponseStatusCode.NotFound;
                    }
                    else
                    {
                        businessModel.StatusCode = ResourceResponseStatusCode.Ok;
                        businessModel.Item = address;
                    }

                }
                catch (Exception ex)
                {
                    businessModel.StatusCode = ResourceResponseStatusCode.ServerError;
                    businessModel.Item = address;

                    Console.WriteLine("AddressResourceAccess SaveAddress() error:" + ex.Message);
                    // Log to applicable logging repository here
                }
            });

            return businessModel;
        }
    }
}
