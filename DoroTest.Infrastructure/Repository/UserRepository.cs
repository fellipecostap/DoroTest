using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using DoroTest.Infrastructure.Common;
using DoroTest.Infrastructure.Context;

namespace DoroTest.Infrastructure.Repository;
public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {

    }
}
