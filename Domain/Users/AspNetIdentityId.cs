﻿using Domain.Primitives;

namespace Domain.Users;
/// <summary>Strongly-typed Id for <see cref="ApplicationUser"/></summary>
public sealed record AspNetIdentityId(long Value) :
    EntityId<ApplicationUser>(Value);