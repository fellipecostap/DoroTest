using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.User.Commands.UpdateUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DoroTest.Application.Services.User.Handlers;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var selectUser = await _userRepository.SelectAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if(selectUser == null)
            throw new NotFoundException(nameof(selectUser));

        selectUser.UserName = request.UserName.IsNullOrEmpty() ? selectUser.UserName : request.UserName;
        var userUpdated = await _userRepository.UpdateAsync(selectUser);
        
        return _mapper.Map<UserDto>(userUpdated);
    }
}
