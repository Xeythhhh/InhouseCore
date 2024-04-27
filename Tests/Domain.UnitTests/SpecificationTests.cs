using Domain.UnitTests.TestImplementations;

using FluentAssertions;

namespace Domain.UnitTests;
public class SpecificationTests
{
    [Fact]
    public void SpecificationWithValidValue_Should_ReturnTrue()
    {
        // Arrange
        TestEntity[] entities = new[]
        {
            TestEntity.Create("1").Value,
            TestEntity.Create("2").Value,
            TestEntity.Create("3").Value
        };
        IQueryable<TestEntity> queryable = entities.AsQueryable();
        TestSpecification specification = new("1");

        // Act
        IQueryable<TestEntity> filteredEntities = queryable.Where(specification.ToExpression());
        bool isSatisfied_true = specification.IsSatisfiedBy(entities[0]);
        bool isSatisfied_false = specification.IsSatisfiedBy(entities[1]);

        // Assert
        filteredEntities.Should().ContainSingle();
        isSatisfied_true.Should().BeTrue();
        isSatisfied_false.Should().BeFalse();
    }

    [Fact]
    public void NotSpecificationWithValidValue_Should_ReturnTrue()
    {
        // Arrange
        TestEntity[] entities = new[]
        {
                TestEntity.Create("1").Value,
                TestEntity.Create("2").Value,
                TestEntity.Create("3").Value,
                TestEntity.Create("4").Value
        };
        IQueryable<TestEntity> queryable = entities.AsQueryable();
        Abstractions.Specification.Specification<TestEntity> notSpecification = new TestSpecification("1").Not();

        // Act
        IQueryable<TestEntity> filteredEntities = queryable.Where(notSpecification.ToExpression());
        bool isSatisfied_true = notSpecification.IsSatisfiedBy(entities[1]);
        bool isSatisfied_false = notSpecification.IsSatisfiedBy(entities[0]);

        // Assert
        filteredEntities.Should().HaveCount(3);
        isSatisfied_true.Should().BeTrue();
        isSatisfied_false.Should().BeFalse();
    }

    [Fact]
    public void OrSpecificationWithValidValue_Should_ReturnTrue()
    {
        // Arrange
        TestEntity[] entities = new[]
        {
                TestEntity.Create("1").Value,
                TestEntity.Create("2").Value,
                TestEntity.Create("3").Value
        };
        IQueryable<TestEntity> queryable = entities.AsQueryable();
        TestSpecification spec1 = new("1");
        TestSpecification spec2 = new("2");
        Abstractions.Specification.Specification<TestEntity> orSpecification = spec1.Or(spec2);

        // Act
        IQueryable<TestEntity> filteredEntities = queryable.Where(orSpecification.ToExpression());
        bool isSatisfied_true = orSpecification.IsSatisfiedBy(entities[0]);
        bool isSatisfied_false = orSpecification.IsSatisfiedBy(entities[2]);

        // Assert
        filteredEntities.Should().HaveCount(2);
        isSatisfied_true.Should().BeTrue();
        isSatisfied_false.Should().BeFalse();
    }

    [Fact]
    public void AndSpecificationWithValidValue_Should_ReturnTrue()
    {
        // Arrange
        TestEntity[] entities = new[]
        {
            TestEntity.Create("1 2").Value,
            TestEntity.Create("1 3").Value,
            TestEntity.Create("2 3").Value,
            TestEntity.Create("1 2 3").Value
        };
        IQueryable<TestEntity> queryable = entities.AsQueryable();
        TestSpecification spec1 = new("1");
        TestSpecification spec2 = new("2");
        Abstractions.Specification.Specification<TestEntity> andSpecification = spec1.And(spec2);

        // Act
        IQueryable<TestEntity> filteredEntities = queryable.Where(andSpecification.ToExpression());
        bool isSatisfied_true = andSpecification.IsSatisfiedBy(entities[0]);
        bool isSatisfied_false = andSpecification.IsSatisfiedBy(entities[1]);

        // Assert
        filteredEntities.Should().HaveCount(2);
        isSatisfied_true.Should().BeTrue();
        isSatisfied_false.Should().BeFalse();
    }
}