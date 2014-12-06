using System.Threading.Tasks;
using Models;
using Models.Address;

namespace AddressManager.Interfaces
{
    public interface IAddressManager
    {
        Task<ResourceResponse<bool>> SaveAddress(Address address);
        Task<ResourceResponse<Address>> GetAddress(int id);
    }
}

