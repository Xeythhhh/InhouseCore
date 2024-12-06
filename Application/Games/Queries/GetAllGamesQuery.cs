using Application.Abstractions;

using Dapper;

using Microsoft.Data.SqlClient;

using SharedKernel.Contracts.v1.Games;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Games.Queries;

public sealed record GetAllGamesQuery : IQuery<GetAllGamesResponse>
{
    internal sealed class Handler(IReadConnectionString connectionString) :
    IQueryHandler<GetAllGamesQuery, GetAllGamesResponse>
    {
        public async Task<Result<GetAllGamesResponse>> Handle(GetAllGamesQuery query, CancellationToken cancellationToken) =>
            await Result.Try(() => new SqlConnection(connectionString.Value))
                .Bind(async connection => await connection.QueryAsync<GameDto>(
                    """
                    SELECT Id, Name
                    FROM Games 
                    """))
                .Map(games => new GetAllGamesResponse(games));
    }
}
