using Microsoft.AspNetCore.Mvc;
using NumeroLetraAPI.API_Setup;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Endpoints.Users;

public class DeleteUser : IWebApi
{
    public void Register(WebApplication app)
    {
        app.MapDelete("/users/delete/{id}", async (int id, [FromServices] IUserRepository userRepository) =>
        {
            var deletedUser = await userRepository.DeleteUser(id);
            return deletedUser;
        })
        .WithMetadata(new EndpointNameMetadata("DeleteUser"))
        .WithTags("Users");
        
    }
}
