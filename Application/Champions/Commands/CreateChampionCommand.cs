using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class CreateChampionCommand(
    string Name,
    string Role,
    IEnumerable<ChampionRestrictionDto>? Restrictions = null) :
    ICommand<Champion.ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<CreateChampionCommand, Champion.ChampionId>
    {
        public async Task<Result<Champion.ChampionId>> Handle(CreateChampionCommand request, CancellationToken cancellationToken) =>
            await Champion.Create(request.Name, request.Role, request.Restrictions)
                .Ensure(repository.CheckIsNameUnique, new ChampionName.NameIsNotUniqueError(request.Name))
                .Bind(champion => repository.Add(champion, cancellationToken)
                    .Map(champion => champion.Id))
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<CreateChampionCommand> FromRequest(CreateChampionRequest dto) =>
        new CreateChampionCommand(dto.Name, dto.Role, dto.Restrictions);
}