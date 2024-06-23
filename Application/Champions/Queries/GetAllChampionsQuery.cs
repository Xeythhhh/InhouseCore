using System.Data.SqlClient;

using Application.Abstractions;

using Dapper;

using FluentResults;

using SharedKernel.Champions;

namespace Application.Champions.Queries;

public sealed record GetAllChampionsQuery : IQuery<List<GetChampionDto>>
{
    internal sealed class Handler(
        ReadConnectionString connectionString) :
        IQueryHandler<GetAllChampionsQuery, List<GetChampionDto>>
    {
        const string sql = "SELECT Id, Name, Role FROM Champions";

        public async Task<Result<List<GetChampionDto>>> Handle(GetAllChampionsQuery request, CancellationToken cancellationToken)
        {
            await using SqlConnection connection = new(connectionString.Value);
            return Result.Ok(await connection.QueryAsync<GetChampionDto>(new CommandDefinition(sql, cancellationToken: cancellationToken)))
                .Map(dtos => dtos.ToList());
        }
    }
}
