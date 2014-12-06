using System;
using System.Threading.Tasks;
using AddressManager.Interfaces;
using Models;
using Models.Address;
using ResourceAccess.Interfaces;

namespace GetAddressById
{
    static class Program
    {
        static void Main(string[] args)
        {
            IAddressManager _addressManager;
            IAddressResourceAccess _addressResourceAccess;


            var addMgr = new AddressManager.AddressManager(_addressResourceAccess);
            var requestedId = 0;
            Console.WriteLine("Enter Address Id to retrieve:");
            var input = Console.ReadLine();
            int.TryParse(input, out requestedId);

            Task<ResourceResponse<Address>> address;
            address = addMgr.GetAddress(requestedId);
            var foo = "bar";
        }
    }
}
