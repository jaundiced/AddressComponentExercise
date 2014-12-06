namespace Models.Address
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public State State {get; set; }
    }
}
