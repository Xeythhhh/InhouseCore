namespace SharedKernel.Contracts.v1.Games;
public sealed record GetAllGamesResponse
    (IEnumerable<GameDto> Games);