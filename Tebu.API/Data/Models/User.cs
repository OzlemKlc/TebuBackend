using Tebu.API.Enums;

namespace Tebu.API.Data.Models
{
    public class User : BaseEntity
    {
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserRole UserRole { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> CustomerOrders { get; set; }
        public virtual ICollection<Order> WorkerOrders { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
