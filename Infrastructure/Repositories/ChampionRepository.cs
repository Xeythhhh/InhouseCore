using Domain.Champions;

using FluentResults;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>Repository for managing Champion entities</summary>
/// <remarks>Initializes a new instance of the <see cref="ChampionRepository"/> class</remarks>
/// <param name="dbContext">The application database context</param>
public partial class ChampionRepository(ApplicationDbContext dbContext) :
    IChampionRepository
{
    /// <summary>Adds a new Champion to the repository<para/>
    /// - <see cref="ErrorMessages.Add"/> when exception is thrown.</summary>
    /// <param name="champion">The Champion to add</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the added Champion</returns>
    public async Task<Result<Champion>> Add(Champion champion, CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.Set<Champion>().AddAsync(champion, cancellationToken);
            return Result.Ok(champion);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ErrorMessages.Add)
                .CausedBy(ex));
        }
    }

    /// <summary>Gets all Champions from the repository<para/>
    /// - <see cref="ErrorMessages.NotFound"/> when the id is not found.<para/>
    /// - <see cref="ErrorMessages.GetAll"/> when exception is thrown.</summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the list of Champions</returns>
    public async Task<Result<List<Champion>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            List<Champion> champions = await dbContext.Set<Champion>().ToListAsync(cancellationToken);
            return champions.Count is not 0
                ? Result.Ok(champions)
                : Result.Fail(ErrorMessages.NotFound);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ErrorMessages.GetAll)
                .CausedBy(ex));
        }
    }

    /// <summary>Gets a Champion by its identifier<para/>
    /// - <see cref="ErrorMessages.NotFound"/> when the id is not found.<para/>
    /// - <see cref="ErrorMessages.Get"/> when exception is thrown.</summary>
    /// <param name="id">The Champion identifier</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the Champion</returns>
    public async Task<Result<Champion>> GetById(ChampionId id, CancellationToken cancellationToken = default)
    {
        try
        {
            Champion? champion = await dbContext.Set<Champion>().FindAsync(
                new object?[] { id },
                cancellationToken: cancellationToken);

            return champion is not null
                ? Result.Ok(champion)
                : Result.Fail(ErrorMessages.NotFound);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ErrorMessages.Get)
                .CausedBy(ex));
        }
    }

    /// <summary>Gets a Champion by a predicate<para/>
    /// - <see cref="ErrorMessages.NotFound"/> when the champion is not found.<para/>
    /// - <see cref="ErrorMessages.Get"/> when exception is thrown.</summary>
    /// <param name="predicate">The search query</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the operation result with the Champions that match the predicate</returns>
    public Result<List<Champion>> GetBy(Func<Champion, bool> predicate)
    {
        try
        {
            List<Champion> champions = dbContext.Set<Champion>().Where(predicate).ToList();
            return champions.Count is not 0
                ? Result.Ok(champions)
                : Result.Fail(ErrorMessages.NotFound);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ErrorMessages.Get)
                .CausedBy(ex));
        }
    }

    /// <summary>Updates an existing Champion in the repository.<para/>
    /// - <see cref="ErrorMessages.Update"/> when exception is thrown.</summary>
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
            return Result.Fail(new Error(ErrorMessages.Update)
                .CausedBy(ex));
        }
    }

    /// <summary>Deletes an existing Champion from the repository<para/>
    /// - <see cref="ErrorMessages.Delete"/> when exception is thrown.</summary>
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
            return Result.Fail(new Error(ErrorMessages.Delete)
                .CausedBy(ex));
        }
    }

    public bool CheckIsNameUnique(Champion champion) => CheckIsNameUnique(champion.Name);
    public bool CheckIsNameUnique(string championName) => !CheckIsNameInUse(championName);

    private bool CheckIsNameInUse(string championName) => dbContext.Set<Champion>()
        .Any(c => c.Name.Equals(championName));
}
