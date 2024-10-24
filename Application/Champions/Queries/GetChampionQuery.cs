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
        public async Task<Result<ChampionDto>> Handle(GetChampionQuery query, CancellationToken cancellationToken)
         => await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await FetchChampion(query, connection, cancellationToken))
                .Bind(async connectionAndChampion => await AddRestrictions(connectionAndChampion, cancellationToken));

        private record ConnectionAndChampion(SqlConnection Connection, ChampionDto Champion);
        private static Task<Result<ConnectionAndChampion>> FetchChampion(GetChampionQuery query, SqlConnection connection, CancellationToken cancellationToken)
         => Result.Try(connection.QuerySingleAsync<ChampionDto>(
                    new CommandDefinition($"SELECT Id, Name, Role, HasRestrictions FROM Champions WHERE Id = {query.ChampionId};", cancellationToken: cancellationToken)))
                .Map(champion => new ConnectionAndChampion(connection, champion));

        private static async Task<Result<ChampionDto>> AddRestrictions(ConnectionAndChampion result, CancellationToken cancellationToken)
         => !result.Champion.HasRestrictions ? result.Champion : await Result
                .Try(async () => await result.Connection.QueryAsync<ChampionRestrictionDto>(
                    new CommandDefinition($"SELECT Id, Target, Reason FROM ChampionRestrictions WHERE ChampionId = {result.Champion.Id};", cancellationToken: cancellationToken)))
                .Map(restrictions => restrictions.ToList())
                .TapIf(restrictions => restrictions.Count > 0, restrictions => result.Champion.Restrictions = restrictions)
                .Map(_ => result.Champion);
    }
}
