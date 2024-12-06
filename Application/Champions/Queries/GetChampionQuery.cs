using Application.Abstractions;

using Dapper;

using Microsoft.Data.SqlClient;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetChampionQuery(long ChampionId) : IQuery<ChampionDto>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
        IQueryHandler<GetChampionQuery, ChampionDto>
    {
        public async Task<Result<ChampionDto>> Handle(GetChampionQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await FetchChampion(query, connection))
                .Bind(AddAugments)
                .Bind(AddRestrictions)
                .Map(result => result.Champion);

        private static Task<Result<ConnectionAndChampion>> FetchChampion(GetChampionQuery query, SqlConnection connection) =>
            Result.Try(GetChampion(query, connection))
                .Map(champion => new ConnectionAndChampion(connection, champion));

        private static Task<Result<ConnectionAndChampion>> AddAugments(ConnectionAndChampion result) =>
           Result.Try(async () => await GetAugments(result))
                .Map(augments => augments.ToList())
                .TapIf(augments => augments.Count > 0,
                    augments => result.Champion.Augments = augments)
                .Map(_ => result);

        private static Task<Result<ConnectionAndChampion>> AddRestrictions(ConnectionAndChampion result) =>
           !result.Champion.HasRestrictions ? Task.FromResult(Result.Ok(result)) : Result
                .Try(async () => await GetRestrictions(result))
                .Map(restrictions => restrictions.ToList())
                .Tap(restrictions => MapAugments(result, restrictions))
                .TapIf(restrictions => restrictions.Count > 0,
                    restrictions => result.Champion.Restrictions = restrictions)
                .Map(_ => result);

        private record ConnectionAndChampion(SqlConnection Connection, ChampionDto Champion);

        private static Task<ChampionDto> GetChampion(GetChampionQuery query, SqlConnection connection) =>
            connection.QuerySingleAsync<ChampionDto>(
                """
                SELECT 
                Id, 
                Name, 
                Role, 
                HasRestrictions, 
                Avatar 
                FROM Champions 
                WHERE Id = @ChampionId
                """, new { query.ChampionId });

        private static Task<IEnumerable<ChampionAugmentDto>> GetAugments(ConnectionAndChampion result) =>
            result.Connection.QueryAsync<ChampionAugmentDto>(
                """
                SELECT
                Id AS AugmentId,
                Name AS AugmentName, 
                Target AS AugmentTarget,
                ColorHex AS AugmentColor,
                Icon AS AugmentIcon
                FROM ChampionAugments 
                WHERE ChampionId = @ChampionId;
                """, new { ChampionId = result.Champion.Id });

        private static Task<IEnumerable<ChampionRestrictionDto>> GetRestrictions(ConnectionAndChampion result) =>
            result.Connection.QueryAsync<ChampionRestrictionDto>(
                """
                SELECT 
                Id AS RestrictionId, 
                AugmentId AS RestrictedAugmentId,
                Augment2Id AS RestrictedComboAugmentId,
                Reason 
                FROM ChampionRestrictions 
                WHERE ChampionId = @ChampionId;
                """, new { ChampionId = result.Champion.Id });

        private static void MapAugments(ConnectionAndChampion result, List<ChampionRestrictionDto> restrictions)
        {
            foreach (ChampionRestrictionDto? restriction in restrictions)
            {
                if (restriction is null) continue;
                if (restriction.RestrictedAugmentId != default)
                {
                    restriction.Augment = result.Champion.Augments.Find(a =>
                        a.AugmentId == restriction.RestrictedAugmentId);
                }
                if (restriction.RestrictedComboAugmentId is not null and not 0)
                {
                    restriction.Combo = result.Champion.Augments.Find(a =>
                        a.AugmentId == restriction.RestrictedComboAugmentId);
                }
            }
        }
    }
}
