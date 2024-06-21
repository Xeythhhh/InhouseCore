using Application.Abstractions;

using Domain.Champions;

using FluentResults;

using SharedKernel.Extensions.ResultExtensions;

namespace Application.Champions.Commands;

public sealed record CreateChampionCommand(
    string Name,
    Champion.Classes Class,
    Champion.Roles Role) :
    ICommand;

internal sealed class CreateChampionCommandHandler(
    IChampionRepository repository,
    IUnitOfWork unitOfWork) :
    ICommandHandler<CreateChampionCommand>
{
    public async Task<Result> Handle(CreateChampionCommand request, CancellationToken cancellationToken) =>
        (await Champion.Create(
            request.Name,
            request.Class,
            request.Role)
        .Ensure(repository.CheckIsNameUnique, Champion.Errors.NameIsNotUnique(request.Name))
        .Ensure(champion => repository.Add(champion, cancellationToken)))
        .OnSuccessTry((token) => unitOfWork.SaveChangesAsync(token), cancellationToken)
        .ToResult();
}
