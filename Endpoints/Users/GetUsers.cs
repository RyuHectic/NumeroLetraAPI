using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Users;

public class GetUsers : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/users", async ([FromServices] IUserRepository userRepository) =>
        {
            var user = await userRepository.GetUsers();
            return Results.Ok(user);
        })
        .WithMetadata(new EndpointNameMetadata("GetUsers"))
        .WithTags("Users");
    }
}
