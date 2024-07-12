using AutoMapper;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Services.User.Commands.GetUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.User.Handlers;
public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, UserVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserVm> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var selectAllUsers = await _userRepository.SelectAllAsync();
        if (selectAllUsers == null)
            throw new NotFoundException(nameof(selectAllUsers));

        return new UserVm
        {
            List = _mapper.Map<IList<UserDto>>(selectAllUsers)
        };
    }
}
