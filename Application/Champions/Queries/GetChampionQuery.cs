using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using SharedKernel.Primitives.Result;

using SharedKernel.Contracts.Responses.Champions;

namespace Application.Champions.Queries;

public sealed record GetChampionQuery(long Id) : IQuery<GetChampionResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
        IQueryHandler<GetChampionQuery, GetChampionResponse>
    {
        private static string Sql(GetChampionQuery query) =>  //probably bad approach TODO learn dapper lol
            $"""
            SELECT Id, Name, Role 
            FROM Champions 
            WHERE Id = {query.Id}
            """;

        public async Task<Result<GetChampionResponse>> Handle(GetChampionQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind<GetChampionResponse>(async connection =>
                    await connection.QuerySingleAsync<GetChampionResponse>(new CommandDefinition(Sql(query), cancellationToken: cancellationToken)));
    }
}
