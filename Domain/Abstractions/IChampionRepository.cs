﻿using Domain.Champions;

using SharedKernel.Primitives.Result;

namespace Domain.Abstractions;

/// <summary>Repository for managing <see cref="Champion"/> entities.</summary>
/// <remarks>This interface is designed for implementing a champion repository.</remarks>
public interface IChampionRepository : IRepository<Champion, Champion.ChampionId>
{
    /// <summary>Checks if the champion's name is unique.</summary>
    /// <param name="champion">The champion to check.</param>
    /// <returns><c>true</c> if the champion's name is unique; otherwise, <c>false</c>.</returns>
    bool IsNameUnique(Champion champion);

    /// <summary>Checks if the champion's name is unique.</summary>
    /// <param name="championName">The name of the champion to check.</param>
    /// <returns><c>true</c> if the champion's name is unique; otherwise, <c>false</c>.</returns>
    bool IsNameUnique(Champion.ChampionName championName);

    /// <summary>Checks if the champion's name is unique.</summary>
    /// <param name="championName">The name of the champion to check.</param>
    /// <returns><c>true</c> if the champion's name is unique; otherwise, <c>false</c>.</returns>
    bool IsNameUnique(string championName);

    /// <summary>Updates the state of the specified champion in the repository.</summary>
    /// <param name="champion">The champion to update.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the update operation.</returns>
    Result Update(Champion champion);

    /// <summary>Removes a specific restriction from a champion.</summary>
    /// <param name="championId">The unique identifier of the champion.</param>
    /// <param name="restrictionId">The unique identifier of the restriction to remove.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the removal operation.</returns>
    Task<Result> RemoveChampionRestriction(Champion.ChampionId championId, Champion.Restriction.RestrictionId restrictionId, CancellationToken cancellationToken);
}
