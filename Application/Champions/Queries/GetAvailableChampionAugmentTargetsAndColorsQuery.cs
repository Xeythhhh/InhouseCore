using Application.Abstractions;

using Dapper;

using Microsoft.Data.SqlClient;

using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetAvailableChampionAugmentTargetsAndColorsQuery(long ChampionId) : IQuery<GetAvailableChampionAugmentTargetsAndColorsResponse>
{
    private record TargetsAndColors(string Augments, string AugmentColors);

    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetAvailableChampionAugmentTargetsAndColorsQuery, GetAvailableChampionAugmentTargetsAndColorsResponse>
    {
        public async Task<Result<GetAvailableChampionAugmentTargetsAndColorsResponse>> Handle(GetAvailableChampionAugmentTargetsAndColorsQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection =>
                {
                    IEnumerable<long> gameIdResponse = await connection.QueryAsync<long>(
                        """
                        SELECT GameId
                        FROM Champions
                        WHERE Id = @ChampionId
                        """, new { query.ChampionId });

                    long? gameId = gameIdResponse.SingleOrDefault();
                    ArgumentNullException.ThrowIfNull(gameId);

                    return await connection.QueryAsync<TargetsAndColors>(
                        """
                        SELECT Augments, AugmentColors 
                        FROM Games 
                        WHERE Id = @GameId;
                        """, new { GameId = gameId });
                })
                .Map(response =>
                {
                    TargetsAndColors? data = response.SingleOrDefault();
                    ArgumentNullException.ThrowIfNull(data);

                    string[] augmentTargets = data.Augments.Split(";");
                    string[] augmentColors = data.AugmentColors.Split(";");
                    return new GetAvailableChampionAugmentTargetsAndColorsResponse(augmentTargets, augmentColors);
                });
    }
}