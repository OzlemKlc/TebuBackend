namespace Tebu.API.Service.DTO
{
    public class VehicleDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public int UserId { get; set; }
    }
}
