using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.User.Commands.GetUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.User.Handlers;
public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var selectUser = await _userRepository.SelectAsync(x => x.Id.Equals(request.Id) || x.UserName.Equals(request.UserName), cancellationToken: cancellationToken);
        if (selectUser == null)
            throw new NotFoundException(nameof(selectUser));

        return _mapper.Map<UserDto>(selectUser);
    }
}
