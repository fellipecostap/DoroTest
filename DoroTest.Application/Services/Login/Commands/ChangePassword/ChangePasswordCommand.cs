using MediatR;

namespace DoroTest.Application.Services.Login.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest
{
    public Guid Id { get; set; }
    public string? NewPassword { get; set; }
}
