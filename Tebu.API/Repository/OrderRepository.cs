using Tebu.API.Data;
using Tebu.API.Data.Models;

namespace Tebu.API.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(TebuDbContext context) : base(context)
        {
        }
    }
}
