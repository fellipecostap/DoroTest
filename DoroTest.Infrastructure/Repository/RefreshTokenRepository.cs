using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using DoroTest.Infrastructure.Common;
using DoroTest.Infrastructure.Context;

namespace DoroTest.Infrastructure.Repository;
public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {

    }
}
