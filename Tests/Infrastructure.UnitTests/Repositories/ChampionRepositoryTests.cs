using Domain.Abstractions;
using Domain.Champions;
using Domain.Primitives;

using FluentAssertions;

using FluentResults;

using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using NSubstitute;

using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Repositories;

public class ChampionRepositoryTestFixture
{
    public List<Champion> Champions = new()
    {
        Champion.Create("Champion 1", Champion.Classes.Melee, Champion.Roles.Dps).Value,
        Champion.Create("Champion 2", Champion.Classes.Ranged, Champion.Roles.Dps).Value,
        Champion.Create("Champion 3", Champion.Classes.Melee, Champion.Roles.Support).Value,
        Champion.Create("Champion 4", Champion.Classes.Ranged, Champion.Roles.Support).Value
    };
}

public class ChampionRepositoryTests(ITestOutputHelper output, ChampionRepositoryTestFixture fixture) :
    IClassFixture<ChampionRepositoryTestFixture>
{
    [Fact]
    public async Task Add_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        Champion champion = Champion.Create("Test", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext dbContext, DbSet<Champion> dbSet, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        dbSet.When(x =>
                x.AddAsync(Arg.Any<Champion>()))
            .Throw(new Exception("Simulated error"));

        // Act
        Result<Champion> result = await repository.Add(champion);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Contain("An error occurred while adding the Champion");

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public async Task Add_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Test", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, DbSet<Champion> _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = await repository.Add(champion);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().BeEquivalentTo(champion);
    }

    [Fact]
    public void Delete_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Champion to delete", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, DbSet<Champion> _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = repository.Delete(champion);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull()
            .And.BeEquivalentTo(champion);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfChampions()
    {
        // Arrange
        (ApplicationDbContext _, DbSet<Champion> _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<List<Champion>> result = await repository.GetAll();

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<List<Champion>>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull()
            .And.BeEquivalentTo(fixture.Champions);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsFailure()
    {
        // Arrange
        ChampionId nonExistingId = new(999);

        (ApplicationDbContext _, DbSet<Champion> _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = await repository.GetById(nonExistingId);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Contain("Champion not found");
    }

    [Fact]
    public void Update_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Updated Champion", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, DbSet<Champion> _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = repository.Update(champion);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull()
            .And.BeEquivalentTo(champion);
    }

    private static DbSet<TEntity> CreateDbSetMock<TEntity>(IEnumerable<TEntity> elements)
        where TEntity : class, IAggregateRoot
    {
        IQueryable<TEntity> queryable = elements.AsQueryable();
        DbSet<TEntity> dbSet = Substitute.For<DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity>>();

        ((IQueryable<TEntity>)dbSet).Provider.Returns(new TestAsyncQueryProvider<TEntity>(queryable.Provider));
        ((IQueryable<TEntity>)dbSet).Expression.Returns(queryable.Expression);
        ((IQueryable<TEntity>)dbSet).ElementType.Returns(queryable.ElementType);
        ((IQueryable<TEntity>)dbSet).GetEnumerator().Returns(queryable.GetEnumerator());
        ((IAsyncEnumerable<TEntity>)dbSet).GetAsyncEnumerator().Returns(new TestAsyncEnumerator<TEntity>(queryable.GetEnumerator()));

        return dbSet;
    }

    private static (ApplicationDbContext, DbSet<TEntity>, TRepository) CreateTestDbContextDbSetAndRepository<TEntity, TRepository>(IEnumerable<TEntity> entities)
        where TRepository : class, IRepository
        where TEntity : class, IAggregateRoot
    {
        ApplicationDbContext dbContext = Substitute.For<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
        DbSet<TEntity> dbSet = CreateDbSetMock(entities);
        dbContext.Set<TEntity>().Returns(dbSet);
        return (dbContext, dbSet, (TRepository)Activator.CreateInstance(typeof(TRepository), dbContext)!);
    }
}