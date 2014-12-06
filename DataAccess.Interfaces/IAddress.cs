using Models.Address;

namespace DataAccess.Interfaces
{
    public interface IAddress
    {
        bool SaveAddress(Address address);
        bool TryGetAddress(int id, out Address address);
    }
}
