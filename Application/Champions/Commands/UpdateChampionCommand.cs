using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;

using SharedKernel.Contracts.Requests.Champions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class UpdateChampionCommand(long Id) : ICommand<ChampionId>
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<UpdateChampionCommand, ChampionId>
    {
        public async Task<Result<ChampionId>> Handle(UpdateChampionCommand request, CancellationToken cancellationToken)
        {
            Result<Champion> getChampionResult = await repository.GetById((ChampionId)request.Id, cancellationToken);

            // TODO
            // only restrictions and assets like image will be updateable

            return repository.Update(getChampionResult.Value)
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken))
                .Map(champion => champion.Id);
        }
    }

    public static Result<UpdateChampionCommand> FromRequest(UpdateChampionRequest dto) =>
        new UpdateChampionCommand(dto.Id);
}