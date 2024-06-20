using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Users;

public class GetUserById : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("users/{id}", async (int Id, [FromServices] IUserRepository userRepository) =>
        {
            var user = await userRepository.GetUserById(Id);
            return user;
        })
        .WithMetadata(new EndpointNameMetadata("GetUserById"))
        .WithTags("Users");
    }
}