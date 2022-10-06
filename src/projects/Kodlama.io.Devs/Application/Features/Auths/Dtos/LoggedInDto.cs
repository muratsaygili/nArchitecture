using Core.Security.JWT;

namespace Application.Features.Auths.Dtos;

public class LoggedInDto
{
    public AccessToken AccessToken { get; set; }
}