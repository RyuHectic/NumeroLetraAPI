using Microsoft.AspNetCore.Http.HttpResults;
using NumeroLetraAPI.Entities;

namespace NumeroLetraAPI.Repository.Interfaces;

public interface IAuthRepository
{
    Task<Results<NotFound<string>, Ok<LoginResponse>>> GetAuthLogin(LoginRequest Login);
}
