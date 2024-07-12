using DoroTest.Domain.Common;

namespace DoroTest.Domain.Entities;
public class UserEntity : AuditableEntity
{
    public string? Password { get; set; }

    public string? UserName { get; set; }

    public string? UserType { get; set; }

}
