using Tebu.API.Data;
using Tebu.API.Data.Models;

namespace Tebu.API.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(TebuDbContext context) : base(context)
        {
        }
    }
}
