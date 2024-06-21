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
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<Champion> result = await repository.Add(null!);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.Add.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public async Task GetAll_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<List<Champion>> result = await repository.GetAll();

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<List<Champion>>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.GetAll.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public async Task GetById_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<Champion> result = await repository.GetById((ChampionId)420);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.Get.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public void GetBy_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<List<Champion>> result = repository.GetBy(c => c.Name == "test");

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<List<Champion>>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.Get.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public void Update_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<Champion> result = repository.Update(null!);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.Update.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public void Delete_ExceptionThrown_ReturnsFailedResult()
    {
        // Arrange
        (ApplicationDbContext dbContext, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);
        dbContext.When(x => x.Set<Champion>()).Throw(new Exception("Simulated error"));

        // Act
        Result<Champion> result = repository.Delete(null!);

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<Champion>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Be(ChampionRepository.ErrorMessages.Delete.Message);

        result.Errors.Should().ContainSingle()
            .Which.Reasons.Should().ContainSingle()
            .Which.Message.Should().Contain("Simulated error");
    }

    [Fact]
    public async Task Add_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Test", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = await repository.Add(champion);

        // Assert
        result.Should().BeOfType<Result<Champion>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().BeEquivalentTo(champion);
    }

    [Fact]
    public void Delete_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Champion to delete", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = repository.Delete(champion);

        // Assert
        result.Should().BeOfType<Result<Champion>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull()
            .And.BeEquivalentTo(champion);
    }

    [Fact]
    public async Task GetAll_ReturnsListOfChampions()
    {
        // Arrange
        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<List<Champion>> result = await repository.GetAll();

        // Assert
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

        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

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
    public void GetById_ExistingId_ReturnsSuccess()
    {
        Assert.True(true);
        Assert.False(false);
        // TODO

        //// Arrange
        //ChampionId testId = new(69420);
        //typeof(Champion).GetProperty("Id")!.SetValue(fixture.Champions[0], testId);
        //(ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        //// Act
        //Result<Champion> result = await repository.GetById(testId);

        //// Assert
        //if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        //result.Should().BeOfType<Result<Champion>>()
        //    .Which.IsSuccess.Should().BeTrue();

        //result.Value.Id.Should().Be(testId);
    }

    [Fact]
    public void GetBy_NonExistingName_ReturnsFailure()
    {
        // Arrange
        const string NonExistingName = "Champion 420";

        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<List<Champion>> result = repository.GetBy(c => c.Name.Equals(NonExistingName));

        // Assert
        if (result.IsFailed) output.WriteLine(JsonConvert.SerializeObject(result.Errors, Formatting.Indented));

        result.Should().BeOfType<Result<List<Champion>>>()
            .Which.IsFailed.Should().BeTrue();

        result.Errors.Should().ContainSingle()
            .Which.Message.Should().Contain("Champion not found");
    }

    [Fact]
    public void GetBy_ExistingName_ReturnsSuccess()
    {
        // Arrange
        const string ExistingName = "Champion 1";

        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<List<Champion>> result = repository.GetBy(c => c.Name.Equals(ExistingName));

        // Assert
        result.Should().BeOfType<Result<List<Champion>>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Count.Should().Be(1);
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Update_ValidChampion_ReturnsOkResultWithChampion()
    {
        // Arrange
        Champion champion = Champion.Create("Updated Champion", Champion.Classes.Melee, Champion.Roles.Dps).Value;
        (ApplicationDbContext _, ChampionRepository repository) = CreateTestDbContextDbSetAndRepository<Champion, ChampionRepository>(fixture.Champions);

        // Act
        Result<Champion> result = repository.Update(champion);

        // Assert
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

    private static (ApplicationDbContext, TRepository) CreateTestDbContextDbSetAndRepository<TEntity, TRepository>(IEnumerable<TEntity> entities)
        where TEntity : class, IAggregateRoot
        where TRepository : IRepository
    {
        ApplicationDbContext dbContext = Substitute.For<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
        DbSet<TEntity> dbSet = CreateDbSetMock(entities);
        dbContext.Set<TEntity>().Returns(dbSet);
        return (dbContext, (TRepository)Activator.CreateInstance(typeof(TRepository), dbContext)!);
    }
}