using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Contracts.v1.Champions.Dtos;
using SharedKernel.Contracts.v1.Champions.Responses;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Queries;

public sealed record GetAllChampionsQuery : IQuery<GetAllChampionsResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetAllChampionsQuery, GetAllChampionsResponse>
    {
        public async Task<Result<GetAllChampionsResponse>> Handle(GetAllChampionsQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await connection.QueryAsync<ChampionDto>(
                    """
                    SELECT Id, Name, Role, HasRestrictions 
                    FROM Champions 
                    """))
                .Map(champions => new GetAllChampionsResponse(champions));
    }
}
