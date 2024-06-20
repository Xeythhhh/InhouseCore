using Domain.Primitives;

namespace Domain.Abstractions;
public interface IRepository<T> where T : IAggregateRoot;
