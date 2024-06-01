namespace Tebu.API.Data.Models
{
    public class Vehicle : BaseEntity
    {
        public int Name { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
