using Tebu.API.Data;
using Tebu.API.Data.Models;

namespace Tebu.API.Repository
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(TebuDbContext context) : base(context)
        {
        }
    }
}
