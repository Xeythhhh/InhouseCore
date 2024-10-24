using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class CreateChampionCommand(
    string Name,
    string Role) :
    ICommand<Champion.ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<CreateChampionCommand, Champion.ChampionId>
    {
        public async Task<Result<Champion.ChampionId>> Handle(CreateChampionCommand command, CancellationToken cancellationToken) =>
            await Champion.Create(command.Name, command.Role)
                .Ensure(repository.IsNameUnique, new Champion.ChampionName.IsNotUniqueError(command.Name))
                .Bind(champion => repository.Add(champion, cancellationToken)
                    .Map(champion => champion.Id))
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<CreateChampionCommand> FromRequest(CreateChampionRequest dto) =>
        new CreateChampionCommand(dto.Name, dto.Role);
}
