using AutoMapper;
using DoroTest.Application.Common.Functions;
using DoroTest.Application.Services.User.Commands.CreateUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;

namespace DoroTest.Application.Services.User.Handlers;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userToCreate = new UserEntity()
        {
            Password = CommonFunctions.CreateMD5Hash(request.Password),
            UserName = request.UserName,
            UserType = request.UserType,
        };

        var userCreated = await _userRepository.InsertAsync(userToCreate);

        return _mapper.Map<UserDto>(userCreated);
    }
}
