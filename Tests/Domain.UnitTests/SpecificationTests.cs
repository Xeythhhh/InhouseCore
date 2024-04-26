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
        var queryable = entities.AsQueryable();
        var specification = new TestSpecification("1");

        // Act
        var filteredEntities = queryable.Where(specification.ToExpression());
        var isSatisfied_true = specification.IsSatisfiedBy(entities[0]);
        var isSatisfied_false = specification.IsSatisfiedBy(entities[1]);

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
        var queryable = entities.AsQueryable();
        var notSpecification = new TestSpecification("1").Not();

        // Act
        var filteredEntities = queryable.Where(notSpecification.ToExpression());
        var isSatisfied_true = notSpecification.IsSatisfiedBy(entities[1]);
        var isSatisfied_false = notSpecification.IsSatisfiedBy(entities[0]);

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
        var queryable = entities.AsQueryable();
        var spec1 = new TestSpecification("1");
        var spec2 = new TestSpecification("2");
        var orSpecification = spec1.Or(spec2);

        // Act
        var filteredEntities = queryable.Where(orSpecification.ToExpression());
        var isSatisfied_true = orSpecification.IsSatisfiedBy(entities[0]);
        var isSatisfied_false = orSpecification.IsSatisfiedBy(entities[2]);

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
        var queryable = entities.AsQueryable();
        var spec1 = new TestSpecification("1");
        var spec2 = new TestSpecification("2");
        var andSpecification = spec1.And(spec2);

        // Act
        var filteredEntities = queryable.Where(andSpecification.ToExpression());
        var isSatisfied_true = andSpecification.IsSatisfiedBy(entities[0]);
        var isSatisfied_false = andSpecification.IsSatisfiedBy(entities[1]);

        // Assert
        filteredEntities.Should().HaveCount(2);
        isSatisfied_true.Should().BeTrue();
        isSatisfied_false.Should().BeFalse();
    }
}