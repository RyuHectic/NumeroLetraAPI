using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NumeroLetraAPI.DbContexts;
using NumeroLetraAPI.Entities;
using NumeroLetraAPI.Helpers;
using NumeroLetraAPI.Models;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly NumeroLetraContext _dbcontext;
    private readonly AuthHelpers _authhelpers;

    public AuthRepository(NumeroLetraContext dbcontext, AuthHelpers authhelpers)
    {
        _dbcontext = dbcontext;
        _authhelpers = authhelpers;
    }

    public async Task<Results<NotFound<string>, Ok<LoginResponse>>> GetAuthLogin(LoginRequest Login)
    {
        return await _dbcontext.TblUsers.FirstOrDefaultAsync(user => user.StrUser == Login.User && user.StrPassword == Login.Password && user.BitActive == true) is User item
                ? TypedResults.Ok(new LoginResponse
                {
                    IdUser = item.IdUser,
                    CompleteName = item.FxCompleteName,
                    User = item.StrUser,
                    Token = _authhelpers.GenerateJWToken(item.IdUser, item.FxCompleteName)
                })
                : TypedResults.NotFound("Usuario no encontrado o no tiene permisos");
    }
}
