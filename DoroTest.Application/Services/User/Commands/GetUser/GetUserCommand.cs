using DoroTest.Application.Services.User.Responses.GetUser;
using MediatR;

namespace DoroTest.Application.Services.User.Commands.GetUser;
public class GetUserCommand : IRequest<UserDto>
{
    public Guid? Id { get; set; }

    public string? UserName { get; set; }
}
