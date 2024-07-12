using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Common.Functions;
using DoroTest.Application.Services.Login.Commands.ChangePassword;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.Login.Handlers;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        #region Update a data to User
        var user = await _userRepository.SelectAsync(l => l.Id.Equals(request.Id), null, true, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(UserEntity));

        user.Password = CommonFunctions.CreateMD5Hash(request.NewPassword);

        var userResponse = await _userRepository.UpdateAsync(user);
        #endregion

        return Unit.Value;
    }
}
