using Tebu.API.Data;
using Tebu.API.Data.Models;

namespace Tebu.API.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>
    {
        public VehicleRepository(TebuDbContext context) : base(context)
        {
        }
    }
}
