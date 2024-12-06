using Application.Abstractions;

using Dapper;

using Microsoft.Data.SqlClient;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetAllChampionsQuery(long GameId) : IQuery<GetAllChampionsResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetAllChampionsQuery, GetAllChampionsResponse>
    {
        public async Task<Result<GetAllChampionsResponse>> Handle(GetAllChampionsQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await connection.QueryAsync<ChampionDto>(
                    """
                    SELECT Id, Name, Role, HasRestrictions, Avatar 
                    FROM Champions 
                    WHERE (@GameId = 0 OR GameId = @GameId)
                    """, new { query.GameId }))
                .Map(champions => new GetAllChampionsResponse(champions));
    }
}
