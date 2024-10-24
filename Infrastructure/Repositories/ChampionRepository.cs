using Domain.Champions;

using FluentAssertions;

using SharedKernel.Primitives.Result;

using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Repositories;

/// <summary>Repository for managing Champion entities</summary>
/// <remarks>Initializes a new instance of the <see cref="ChampionRepository"/> class</remarks>
/// <param name="dbContext">The application database context</param>
public partial class ChampionRepository(ApplicationDbContext dbContext) :
    IChampionRepository
{
    public async Task<Result<Champion>> Add(Champion champion, CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.Champions.AddAsync(champion, cancellationToken);

            return Result.Ok(champion);
        }
        catch (Exception ex)
        {
            return Result.Fail(new AddError()
                .CausedBy(ex));
        }
    }

    public async Task<Result<List<Champion>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            List<Champion> champions = await dbContext.Champions
                .Include(champion => champion.Restrictions)
                .ToListAsync(cancellationToken);

            return Result.Ok(champions);
        }
        catch (Exception ex)
        {
            return Result.Fail(new GetAllError()
                .CausedBy(ex));
        }
    }

    public async Task<Result<List<TOut>>> GetAll<TOut>(Func<Champion, TOut> converter, CancellationToken cancellationToken = default)
    {
        try
        {
            List<TOut> champions = (await dbContext.Champions
                .Include(champion => champion.Restrictions)
                .ToListAsync(cancellationToken)) // TODO Register conversion before materialization for performance
                .ConvertAll(c => converter(c));

            return Result.Ok(champions);
        }
        catch (Exception ex)
        {
            return Result.Fail(new GetAllError()
                .CausedBy(ex));
        }
    }

    public async Task<Result<Champion>> GetById(Champion.ChampionId id, CancellationToken cancellationToken = default)
    {
        try
        {
            Champion? champion = await dbContext.Champions
            .Include(champion => champion.Restrictions)
            .FirstOrDefaultAsync(c => Equals(c.Id, id), cancellationToken);

            return champion is not null
                ? Result.Ok(champion)
                : Result.Fail(new NotFoundError());
        }
        catch (Exception ex)
        {
            return Result.Fail(new GetError()
                .CausedBy(ex));
        }
    }

    public Result<List<Champion>> GetBy(Func<Champion, bool> predicate)
    {
        try
        {
            List<Champion> champions = dbContext.Champions
                .Include(champion => champion.Restrictions)
                .Where(predicate).ToList();

            return champions.Count != 0
                ? Result.Ok(champions)
                : Result.Fail(new NotFoundError());
        }
        catch (Exception ex)
        {
            return Result.Fail(new GetError()
                .CausedBy(ex));
        }
    }

    public Result Update(Champion champion)
    {
        try
        {
            dbContext.Champions.Update(champion);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new UpdateError()
                .CausedBy(ex));
        }
    }

    public Result Delete(Champion champion)
    {
        try
        {
            if (champion.Restrictions.Count != 0)
            {
                foreach (Champion.Restriction restriction in champion.Restrictions)
                    dbContext.ChampionRestrictions.Remove(restriction);
            }

            dbContext.Champions.Remove(champion);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new DeleteError()
                .CausedBy(ex));
        }
    }

    public bool IsNameUnique(Champion champion) => IsNameUnique(champion.Name);
    public bool IsNameUnique(Champion.ChampionName championName) => IsNameUnique(championName.Value);
    public bool IsNameUnique(string championName) => !IsNameInUse(championName);

    private bool IsNameInUse(string championName) =>
            dbContext.Champions.AsEnumerable().Any(champion => champion.Name == championName);

    public async Task<Result> RemoveChampionRestriction(
        Champion.ChampionId championId,
        Champion.Restriction.RestrictionId restrictionId,
        CancellationToken cancellationToken)
    {
        try
        {
            Champion.Restriction? restriction = await dbContext.ChampionRestrictions.FindAsync(new object?[] { restrictionId }, cancellationToken: cancellationToken);
            if (restriction is null) return Result.Fail(new RemoveRestrictionError("Restriction not found."));

            Champion? champion = await dbContext.Champions
                .Include(c => c.Restrictions)
                .FirstOrDefaultAsync(c => c.Id == championId, cancellationToken);
            if (champion is null) return Result.Fail(new RemoveRestrictionError("Champion not found."));

            champion.Restrictions.Remove(restriction);
            dbContext.ChampionRestrictions.Remove(restriction);

            if (champion.Restrictions.Count == 0) champion.HasRestrictions = false;

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new DeleteRestrictionError()
                .CausedBy(ex));
        }
    }

    public async Task<Result<Champion.Restriction>> GetRestrictionById(Champion.Restriction.RestrictionId restrictionId, CancellationToken cancellationToken)
    {
        try
        {
            Champion.Restriction? restriction = await dbContext.ChampionRestrictions.FindAsync(new object?[] { restrictionId }, cancellationToken: cancellationToken);
#pragma warning disable RCS1084 // Use coalesce expression instead of conditional expression
            return restriction is null
                ? Result.Fail(new EditRestrictionError("Restriction not found."))
                : restriction;
#pragma warning restore RCS1084 // Use coalesce expression instead of conditional expression
        }
        catch (Exception ex)
        {
            return Result.Fail(new DeleteRestrictionError()
                .CausedBy(ex));
        }
    }
}
