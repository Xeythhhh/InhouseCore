using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetChampionAugmentNamesQuery(long ChampionId) : IQuery<GetChampionAugmentNamesResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetChampionAugmentNamesQuery, GetChampionAugmentNamesResponse>
    {
        public async Task<Result<GetChampionAugmentNamesResponse>> Handle(GetChampionAugmentNamesQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await connection.QueryAsync<string>(
                $"""
                    SELECT Name 
                    FROM ChampionAugments 
                    WHERE ChampionId = {query.ChampionId};
                    """))
                .Map(augmentNames => new GetChampionAugmentNamesResponse(augmentNames));
    }
}