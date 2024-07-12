using DoroTest.Domain.Common;

namespace DoroTest.Domain.Entities;
public class RefreshTokenEntity : AuditableEntity
{
    public UserEntity UserEntity { get; set; }

    public string? RefreshToken { get; set; }
}
