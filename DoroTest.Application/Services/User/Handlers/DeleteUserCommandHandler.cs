using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Common.Models;
using DoroTest.Application.Services.User.Commands.DeleteUser;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DoroTest.Application.Services.User.Handlers;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, IOptions<Authentication> options,
         IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var selectUser = await _userRepository.SelectAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if (selectUser == null)
            throw new NotFoundException(nameof(selectUser));

        await _userRepository.DeleteAsync(x => x.Id.Equals(selectUser.Id));

        return Unit.Value;
    }
}
