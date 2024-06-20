using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Entities;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Auths;

public class GetLogin : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapPost("/AuthUser", async ([FromBody] LoginRequest login, [FromServices] IAuthRepository AuthRepository) =>
        {
            var userLogin = await AuthRepository.GetAuthLogin(login);

            return userLogin;

        })
        .WithMetadata(new EndpointNameMetadata("GetAuth"))
        .WithTags("Auth");
    }
}
