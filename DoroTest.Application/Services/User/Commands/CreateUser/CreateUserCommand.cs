using DoroTest.Application.Services.User.Responses.GetUser;
using MediatR;

namespace DoroTest.Application.Services.User.Commands.CreateUser;
public class CreateUserCommand : IRequest<UserDto>
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? UserType { get; set; }
}
