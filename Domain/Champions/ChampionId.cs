using Domain.Abstractions;
using Domain.Errors;
using Domain.Primitives;
using Domain.Primitives.Result;

using FluentValidation;
using FluentValidation.Results;

namespace Domain.Champions;

/// <summary>Strongly-typed Id for <see cref="Champion"/>.</summary>
public sealed record ChampionId(long Value) :
    EntityId<Champion>(Value);
