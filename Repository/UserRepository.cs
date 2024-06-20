using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NumeroLetraAPI.DbContexts;
using NumeroLetraAPI.Models;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly NumeroLetraContext _dbcontext;

    public UserRepository(NumeroLetraContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task Save()
    {
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _dbcontext.TblUsers.OrderByDescending(user => user.IdUser).ToListAsync();
    }

    async Task<User?> GetUser(int Id) => await _dbcontext.TblUsers.FirstOrDefaultAsync(user => user.IdUser == Id);

    public async Task<Results<NotFound<string>, Ok<User>>> GetUserById(int IdUser)
    {
        User? userResponse = await GetUser(IdUser);

        return userResponse == null ? TypedResults.NotFound("Usuario no encontrado.") : TypedResults.Ok(userResponse);
    }

    public async Task<Ok<string>> InsertUser(User user)
    {
        _dbcontext.TblUsers.Add(user);
        await Save();

        return TypedResults.Ok($"Usuario {user.StrUser} insertado con exito.");
    }

    public async Task<Results<NotFound<string>, Ok<string>>> UpdateUser(User user)
    {
        User? selectedUser = await GetUser(user.IdUser);

        if (selectedUser == null)
            return TypedResults.NotFound("Usuario no encontrado.");

        selectedUser.StrName = user.StrName;
        selectedUser.StrLastName = user.StrLastName;
        selectedUser.StrUser = user.StrUser;
        selectedUser.StrPassword = user.StrPassword;
        selectedUser.BitActive = user.BitActive;

        await Save();

        return TypedResults.Ok($"Usuario {selectedUser.FxCompleteName} actualizado con exito.");
    }

    public async Task<Results<NotFound<string>, Ok<string>>> DeleteUser(int IdUser)
    {
        User? selectedUser = await GetUser(IdUser);

        if (selectedUser == null)
            return TypedResults.NotFound("Usuario no encontrado.");

        _dbcontext.TblUsers.Remove(selectedUser);
        await Save();

        return TypedResults.Ok($"Usuario {selectedUser.FxCompleteName} eliminado.");
    }
}
