using AutoMapper;
using DoroTest.Application.Common.Authorization;
using DoroTest.Application.Common.Exceptions;
using DoroTest.Application.Common.Functions;
using DoroTest.Application.Common.Models;
using DoroTest.Application.Services.Login.Commands.LoginUser;
using DoroTest.Application.Services.Login.Reponses.LoginUser;
using DoroTest.Domain.Entities;
using DoroTest.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Options;

namespace DoroTest.Application.Services.Login.Handlers;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly Authentication _SecretKey;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IOptions<Authentication> options, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _SecretKey = options.Value;
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
    }

    public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(await _userRepository.SelectAsync(b => b.Id.Equals(request.Id) && b.Password.Equals(CommonFunctions.CreateMD5Hash(request.Password)), cancellationToken: cancellationToken));

        if (user == null)
            throw new ValidationException("Erro no login", request.Id.ToString());

        #region Generate token, refresh token and verify 
        var authenticated = Auth.GenerateToken(user, _SecretKey.SecretKey);

        var RefreshToken = Auth.GenerateRefreshToken();

        var RefreshTokenToCreate = new RefreshTokenEntity
        {
            UserEntity = user,
            RefreshToken = RefreshToken
        };

        await _refreshTokenRepository.DeleteAsync(b => b.UserEntity.Id.Equals(user.Id), cancellationToken);
        await _refreshTokenRepository.InsertAsync(RefreshTokenToCreate, cancellationToken);

        authenticated.RefreshToken = RefreshToken;
        #endregion

        return authenticated;
    }
}
