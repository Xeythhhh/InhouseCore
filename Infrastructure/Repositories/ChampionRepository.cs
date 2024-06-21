using Domain.Champions;

using FluentResults;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>Repository for managing Champion entities</summary>
/// <remarks>Initializes a new instance of the <see cref="ChampionRepository"/> class</remarks>
/// <param name="dbContext">The application database context</param>
public class ChampionRepository(ApplicationDbContext dbContext) :
    IChampionRepository
{
    /// <summary>Adds a new Champion to the repository</summary>
    /// <param name="champion">The Champion to add</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the added Champion</returns>
    public async Task<Result<Champion>> Add(Champion champion)
    {
        try
        {
            await dbContext.Set<Champion>().AddAsync(champion);
            return Result.Ok(champion);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while adding the Champion")
                .CausedBy(ex));
        }
    }

    /// <summary>Gets all Champions from the repository</summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the list of Champions</returns>
    public async Task<Result<List<Champion>>> GetAll()
    {
        try
        {
            List<Champion> champions = await dbContext.Set<Champion>().ToListAsync();
            return Result.Ok(champions);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while retrieving Champions")
                .CausedBy(ex));
        }
    }

    /// <summary>Gets a Champion by its identifier</summary>
    /// <param name="id">The Champion identifier</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the Champion</returns>
    public async Task<Result<Champion>> GetById(ChampionId id)
    {
        try
        {
            Champion? champion = await dbContext.Set<Champion>().FindAsync(id);
            return champion is not null
                ? Result.Ok(champion)
                : Result.Fail(new Error("Champion not found"));
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while retrieving the Champion")
                .CausedBy(ex));
        }
    }

    /// <summary>Gets a Champion by its name</summary>
    /// <param name="name">The Champion name</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the Champion</returns>
    public async Task<Result<Champion>> GetByName(string name)
    {
        try
        {
            Champion? champion = await dbContext.Set<Champion>().FirstOrDefaultAsync(c => c.Name == name);
            return champion is not null
                ? Result.Ok(champion)
                : Result.Fail(new Error("Champion not found"));
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while retrieving the Champion")
                .CausedBy(ex));
        }
    }

    /// <summary>Updates an existing Champion in the repository</summary>
    /// <param name="champion">The Champion to update</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the updated Champion</returns>
    public Result<Champion> Update(Champion champion)
    {
        try
        {
            dbContext.Set<Champion>().Update(champion);
            return Result.Ok(champion);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while updating the Champion")
                .CausedBy(ex));
        }
    }

    /// <summary>Deletes an existing Champion from the repository</summary>
    /// <param name="champion">The Champion to delete</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the deleted Champion</returns>
    public Result<Champion> Delete(Champion champion)
    {
        try
        {
            dbContext.Set<Champion>().Remove(champion);
            return Result.Ok(champion);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("An error occurred while deleting the Champion")
                .CausedBy(ex));
        }
    }
}
