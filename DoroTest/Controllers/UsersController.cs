using DoroTest.Application.Services.Login.Commands.ChangePassword;
using DoroTest.Application.Services.Login.Commands.LoginUser;
using DoroTest.Application.Services.Login.Reponses.LoginUser;
using DoroTest.Application.Services.User.Commands.CreateUser;
using DoroTest.Application.Services.User.Commands.DeleteUser;
using DoroTest.Application.Services.User.Commands.GetUser;
using DoroTest.Application.Services.User.Commands.UpdateUser;
using DoroTest.Application.Services.User.Responses.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoroTest.Controllers;

public class UsersController : ApiControllerBase
{
    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpGet()]
    public async Task<ActionResult<UserVm>> GetAll()
    {
        return await Mediator.Send(new GetAllUsersCommand());
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetUserCommand() { Id = id });
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpDelete]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
        return await Mediator.Send(new DeleteUserCommand() { Id = id });
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult<UserDto>> Update(UpdateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize("Admin")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserDto>> Post(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("ChangePassword")]
    public async Task<ActionResult> Post(ChangePasswordCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("Login")]
    public async Task<ActionResult<LoginDto>> Post(LoginUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
