using System.Threading.Tasks;
using Models;
using Models.Address;

namespace ResourceAccess.Interfaces
{
    public interface IAddressResourceAccess
    {
        Task<ResourceResponse<bool>> SaveAddress(Address address);
        Task<ResourceResponse<Address>> GetAddress(int id);
    }
}
