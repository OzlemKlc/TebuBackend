namespace Tebu.API.Data.Models
{
    public class Address : BaseEntity
    {
        public string Name { get; set; }
        public string FullAdress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
