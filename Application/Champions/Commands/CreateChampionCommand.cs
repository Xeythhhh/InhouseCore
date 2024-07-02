using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;

using SharedKernel.Contracts.Requests.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class CreateChampionCommand(string Name, string Role) : ICommand<ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<CreateChampionCommand, ChampionId>
    {
        public async Task<Result<ChampionId>> Handle(CreateChampionCommand request, CancellationToken cancellationToken) =>
            await Champion.Create(request.Name, request.Role)
                .Ensure(repository.CheckIsNameUnique, new ChampionName.NameIsNotUniqueError(request.Name))
                .Bind(champion => repository.Add(champion, cancellationToken)
                    .Map(champion => champion.Id))
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<CreateChampionCommand> FromRequest(CreateChampionRequest dto) =>
        new CreateChampionCommand(dto.Name, dto.Role);
}