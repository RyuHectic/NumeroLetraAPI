using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NumeroLetraAPI.DbContexts;
using NumeroLetraAPI.Models;
using NumeroLetraAPI.Repository.Interfaces;

namespace NumeroLetraAPI.Repository;

public class LogRepository : ILogRepository
{
    private readonly NumeroLetraContext _dbcontext;

    public LogRepository(NumeroLetraContext dbcontext)
    {  
        _dbcontext = dbcontext;
    }

    public async Task Save()
    {
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Log>> GetLog()
    {
        return await _dbcontext.TblLogs.OrderByDescending(user => user.IdLog).ToListAsync();
    }

    public async Task<Ok<string>> InsertLog(Log log)
    {
        _dbcontext.TblLogs.Add(log);
        await Save();

        return TypedResults.Ok($"resgitro {log.IntNumber} del log insertado con exito.");
    }
}
