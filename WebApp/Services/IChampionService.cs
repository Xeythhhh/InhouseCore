using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Primitives.Result;
using WebApp.Champions.Augments.Models;
using WebApp.Champions.Restrictions.Models;

namespace WebApp.Services
{
    /// <summary> Defines the methods for interacting with champion-related services.</summary>
    public interface IChampionService
    {
        /// <summary> Retrieves all champions asynchronously.</summary>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result{GetAllChampionsResponse}"/> containing the list of champions or error information.</returns>
        Task<Result<GetAllChampionsResponse>> GetChampionsAsync(CancellationToken cancellationToken = default);

        /// <summary> Retrieves a champion by its identifier asynchronously.</summary>
        /// <param name="id">The identifier of the champion to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result{ChampionDto}"/> containing the champion details or error information.</returns>
        Task<Result<ChampionDto>> GetChampionByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<Result> CreateChampionAsync(string name, string role, CancellationToken cancellationToken = default);

        /// <summary> Deletes a champion asynchronously by its identifier.</summary>
        /// <param name="id">The identifier of the champion to delete.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Task<Result> DeleteChampionAsync(string id, CancellationToken cancellationToken = default);

        Task<Result> AddAugmentAsync(AddAugmentModel model, CancellationToken cancellationToken = default);
        Task<Result> UpdateAugmentAsync(UpdateAugmentModel model, CancellationToken cancellationToken = default);

        /// <summary> Removes an augment from a champion asynchronously.</summary>
        /// <param name="championId">The identifier of the champion from whom to remove the augment.</param>
        /// <param name="augmentId">The identifier of the augment to remove.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Task<Result> RemoveAugmentAsync(string championId, string augmentId, CancellationToken cancellationToken = default);

        /// <summary> Retrieves the augment names for a specified champion asynchronously.</summary>
        /// <param name="championId">The identifier of the champion whose augment names to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result{GetChampionAugmentNamesResponse}"/> containing the augment names or error information.</returns>
        Task<Result<GetChampionAugmentNamesResponse>> GetAugmentNamesAsync(string championId, CancellationToken cancellationToken = default);

        Task<Result> AddRestrictionAsync(AddRestrictionModel model, CancellationToken cancellationToken = default);
        Task<Result> UpdateRestrictionAsync(UpdateRestrictionModel model, CancellationToken cancellationToken = default);

        /// <summary> Removes a restriction from a champion asynchronously.</summary>
        /// <param name="championId">The identifier of the champion from whom to remove the restriction.</param>
        /// <param name="restrictionId">The identifier of the restriction to remove.</param>
        /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
        Task<Result> RemoveRestrictionAsync(string championId, string restrictionId, CancellationToken cancellationToken = default);
    }
}
