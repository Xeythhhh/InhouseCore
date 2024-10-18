using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetChampionQuery(long Id) : IQuery<ChampionDto>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
        IQueryHandler<GetChampionQuery, ChampionDto>
    {
        private static string Sql(GetChampionQuery query) =>  //probably bad approach TODO learn dapper lol
            $"""
            SELECT Id, Name, Role 
            FROM Champions
            WHERE Id = {query.Id};

            Select Id, DefaultKey, Name, Reason
            FROM ChampionRestrictions
            WHERE ChampionId = {query.Id}
            """;

        public async Task<Result<ChampionDto>> Handle(GetChampionQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(connection => connection.QueryMultipleAsync(new CommandDefinition(Sql(query), cancellationToken: cancellationToken)))
                .Map(gridReader =>
                {
                    ChampionDto? champion = gridReader.Read<ChampionDto>().SingleOrDefault();
                    if (champion is null) return null;
                    champion.Restrictions = gridReader.Read<ChampionRestrictionDto>().ToList();
                    return champion;
                })
                .Ensure(champion => champion != null, new Error($"Champion with id `{query.Id}` not found."))
                .Map(championOrNull => championOrNull!);
    }
}
