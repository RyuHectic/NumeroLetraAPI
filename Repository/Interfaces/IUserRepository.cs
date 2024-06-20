using Microsoft.AspNetCore.Http.HttpResults;
using NumeroLetraAPI.Models;

namespace NumeroLetraAPI.Repository.Interfaces;

public interface IUserRepository
{
    Task Save();

    Task<IEnumerable<User>> GetUsers();

    Task<Results<NotFound<string>, Ok<User>>> GetUserById(int IdUser);

    Task<Ok<string>> InsertUser(User user);

    Task<Results<NotFound<string>, Ok<string>>> UpdateUser(User user);

    Task<Results<NotFound<string>, Ok<string>>> DeleteUser(int IdUser);
}
