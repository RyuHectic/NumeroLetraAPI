using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Models;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Users;

public class UpdateUser : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapPut("/users/update", async ([FromBody] User user, [FromServices] IUserRepository userRepository) =>
        {
            var updatedUser = await userRepository.UpdateUser(user);
            return updatedUser;
        })
        .WithMetadata(new EndpointNameMetadata("UpdateUser"))
        .WithTags("Users");
    }
}
