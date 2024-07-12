using DoroTest.Application.Services.User.Responses.GetUser;
using MediatR;

namespace DoroTest.Application.Services.User.Commands.UpdateUser;
public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid? Id { get; set; }
    public string? UserName { get; set; }
}
