using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class UpdateChampionCommand(
    long Id,
    IEnumerable<ChampionRestrictionDto>? Restrictions = null) :
    ICommand<Champion.ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<UpdateChampionCommand, Champion.ChampionId>
    {
        public async Task<Result<Champion.ChampionId>> Handle(UpdateChampionCommand request, CancellationToken cancellationToken) =>
            await repository.GetById((Champion.ChampionId)request.Id, cancellationToken)
                .OnSuccessTry(champion =>
                    champion.Restrictions = request.Restrictions?
                        .Select(r => new ChampionRestriction()
                        {
                            DefaultKey = r.DefaultKey,
                            Name = r.Name,
                            Reason = r.Reason
                        }).ToList() ?? new List<ChampionRestriction>())
                .OnSuccessTry(champion => repository.Update(champion))
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken))
                .Map(champion => champion.Id);
    }

    public static Result<UpdateChampionCommand> FromRequest(UpdateChampionRequest dto) =>
        new UpdateChampionCommand(dto.Id, dto.Restrictions);
}