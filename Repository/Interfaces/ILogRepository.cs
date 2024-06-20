using Microsoft.AspNetCore.Http.HttpResults;
using NumeroLetraAPI.Models;

namespace NumeroLetraAPI.Repository.Interfaces;

public interface ILogRepository
{
    Task<IEnumerable<Log>> GetLog();

    Task<Ok<string>> InsertLog(Log log);
}
