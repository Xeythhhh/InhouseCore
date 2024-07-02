using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Primitives.Result;

using SharedKernel.Contracts.Responses.Champions;
using SharedKernel.Extensions.ResultExtensions;

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
                .Bind(connection => connection.QueryAsync<GetAllChampionsResponse.ChampionDto>(new CommandDefinition(sql, cancellationToken: cancellationToken)))
                .Map(dtos => new GetAllChampionsResponse(dtos));
    }
}
