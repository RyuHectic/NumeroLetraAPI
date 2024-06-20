using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Models;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Users;

public class InsertUser : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapPost("/users/insert", async ([FromBody] User user, [FromServices] IUserRepository userRepository) =>
        {
            var insertedUser = await userRepository.InsertUser(user);
            return insertedUser;
        })
        .WithMetadata(new EndpointNameMetadata("InsertUser"))
        .WithTags("Users");
    }
}
