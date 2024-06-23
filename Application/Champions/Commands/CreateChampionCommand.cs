using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;

using FluentResults;
using FluentResults.Extensions;

using Microsoft.Extensions.Logging;

using SharedKernel.Champions;
using SharedKernel.Extensions.ResultExtensions;

namespace Application.Champions.Commands;

public sealed record class CreateChampionCommand(
    string Name,
    string Role) :
    ICommand<ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<CreateChampionCommand, ChampionId>
    {
        public async Task<Result<ChampionId>> Handle(CreateChampionCommand request, CancellationToken cancellationToken) =>
            await Champion.Create(
                request.Name,
                request.Role)
            .Ensure(repository.CheckIsNameUnique, ChampionName.Errors.NameIsNotUnique(request.Name))
            .Bind(champion => repository.Add(champion, cancellationToken)
                .Map(champion => champion.Id))
            .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<CreateChampionCommand> FromDto(CreateChampionDto dto) =>
        new CreateChampionCommand(dto.Name, dto.Role);
}