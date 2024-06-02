namespace Tebu.API.Service.DTO
{
    public class AddressDTO : BaseDTO
    {

        public string Name { get; set; }
        public string FullAdress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int UserId { get; set; }


    }
}
