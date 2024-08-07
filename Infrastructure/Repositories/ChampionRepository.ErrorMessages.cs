﻿using SharedKernel.Primitives.Reasons;

namespace Infrastructure.Repositories;

public partial class ChampionRepository
{
    public sealed class GetAllError() : Error("An error occurred while retrieving Champions");
    public sealed class GetError() : Error("An error occurred while retrieving Champion");
    public sealed class AddError() : Error("An error occurred while adding the Champion");
    public sealed class UpdateError() : Error("An error occurred while updating the Champion");
    public sealed class DeleteError() : Error("An error occurred while deleting Champions");
    public sealed class NotFoundError() : Error("Champion not found");
}
