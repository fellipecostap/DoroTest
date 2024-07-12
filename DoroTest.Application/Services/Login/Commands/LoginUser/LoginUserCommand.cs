using MediatR;
using DoroTest.Application.Services.Login.Reponses.LoginUser;

namespace DoroTest.Application.Services.Login.Commands.LoginUser;
public class LoginUserCommand : IRequest<LoginDto>
{
    public Guid? Id { get; set; }

    public string? Password { get; set; }
}
