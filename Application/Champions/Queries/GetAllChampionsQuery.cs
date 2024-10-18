using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Contracts.v1.Champions;

namespace Application.Champions.Queries;

public sealed record GetAllChampionsQuery : IQuery<GetAllChampionsResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
        IQueryHandler<GetAllChampionsQuery, GetAllChampionsResponse>
    {
        const string sql =
            """
            SELECT Id, Name, Role 
            FROM Champions
            """;

        public async Task<Result<GetAllChampionsResponse>> Handle(GetAllChampionsQuery query, CancellationToken cancellationToken) =>
            await Result
                .Try(() => new SqlConnection(connectionString.Value))
                .Bind(connection => connection.QueryAsync<ChampionDto>(new CommandDefinition(sql, cancellationToken: cancellationToken)))
                .Map(dtos => new GetAllChampionsResponse(dtos));
    }
}
