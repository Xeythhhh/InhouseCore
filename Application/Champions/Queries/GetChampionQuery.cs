using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Contracts.v1.Champions;
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
                .Bind(async connection => await FetchChampion(query, connection, cancellationToken))
                .Bind(async connectionAndChampion => await AddRestrictions(connectionAndChampion, cancellationToken));

        private static Task<Result<ConnectionAndChampion>> FetchChampion(GetChampionQuery query, SqlConnection connection, CancellationToken cancellationToken) =>
            Result.Try(GetChampion(query, connection, cancellationToken))
                .Map(champion => new ConnectionAndChampion(connection, champion));

        private static Task<Result<ChampionDto>> AddRestrictions(ConnectionAndChampion result, CancellationToken cancellationToken) =>
           !result.Champion.HasRestrictions ? Task.FromResult(Result.Ok(result.Champion)) : Result
               .Try(async () => await GetRestrictions(result, cancellationToken))
               .Map(restrictions => restrictions.ToList())
               .TapIf(restrictions => restrictions.Count > 0, restrictions => result.Champion.Restrictions = restrictions)
               .Map(_ => result.Champion);

        private record ConnectionAndChampion(SqlConnection Connection, ChampionDto Champion);

        private static Task<ChampionDto> GetChampion(GetChampionQuery query, SqlConnection connection, CancellationToken cancellationToken) =>
            connection.QuerySingleAsync<ChampionDto>(new CommandDefinition(
                $"SELECT Id, Name, Role, HasRestrictions FROM Champions WHERE Id = {query.ChampionId};",
                cancellationToken: cancellationToken));

        private static Task<IEnumerable<ChampionRestrictionDto>> GetRestrictions(ConnectionAndChampion result, CancellationToken cancellationToken) =>
            result.Connection.QueryAsync<ChampionRestrictionDto>(new CommandDefinition(
                $"SELECT Id AS RestrictionId, AbilityName, Identifier, ColorHex AS Color, Reason FROM ChampionRestrictions WHERE ChampionId = {result.Champion.Id};",
                cancellationToken: cancellationToken));
    }
}
