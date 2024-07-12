using MediatR;

namespace DoroTest.Application.Services.User.Commands.DeleteUser;
public class DeleteUserCommand : IRequest<Unit>
{
    public Guid? Id { get; set; }
}
