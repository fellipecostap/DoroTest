using DoroTest.Application.Common.Mappings;
using DoroTest.Domain.Entities;

namespace DoroTest.Application.Services.User.Responses.GetUser;
public class UserDto : IMapFrom<UserEntity>
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
