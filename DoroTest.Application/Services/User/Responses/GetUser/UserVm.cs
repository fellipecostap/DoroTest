namespace DoroTest.Application.Services.User.Responses.GetUser;
public class UserVm
{
    public IList<UserDto> List { get; set; } = new List<UserDto>();
}
