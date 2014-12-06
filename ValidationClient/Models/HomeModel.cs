using System.Collections.Generic;
using Models.Address;

namespace ValidationClient.Models
{
    public class HomeModel
    {
        public string Message { get; set; }
        public Address SelectedAddress { get; set; }
    }
}