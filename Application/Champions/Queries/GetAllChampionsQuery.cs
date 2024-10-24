using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetAllChampionsQuery : IQuery<GetAllChampionsResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetAllChampionsQuery, GetAllChampionsResponse>
    {
        const string sql =
            """
            SELECT
                c.Id,
                c.Name,
                c.Role,
                c.HasRestrictions,
                r.Id AS RestrictionId,
                r.AbilityName,
                r.Identifier,
                r.ColorHex AS Color,
                r.Reason
            FROM
                Champions c
            LEFT JOIN
                ChampionRestrictions r ON c.Id = r.ChampionId
            """;

        public async Task<Result<GetAllChampionsResponse>> Handle(GetAllChampionsQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await connection.QueryAsync<ChampionDto, ChampionRestrictionDto, ChampionDto>(
                    sql, (champion, restriction) =>
                    {
                        if (!IsRestrictionNullOrEmpty(restriction)) champion.Restrictions.Add(restriction);
                        return champion;
                    },
                    splitOn: "RestrictionId"))
                .Map(champions => new GetAllChampionsResponse(
                    champions.GroupBy(champion => champion.Id)
                        .Select(group =>
                        {
                            ChampionDto result = group.First();
                            result.Restrictions = group.SelectMany(c => c.Restrictions)
                                .DistinctBy(r => r.RestrictionId)
                                .ToList();

                            return result;
                        })));

        private static bool IsRestrictionNullOrEmpty(ChampionRestrictionDto restriction) => restriction is null || restriction.RestrictionId is null;
    }
}