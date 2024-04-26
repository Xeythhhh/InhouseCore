using Domain.UnitTests;

using FluentAssertions;

namespace Application.UnitTests;
public class SpecificationTests
{
    [Fact]
    public void SpecificationDevelopmentDriver()
    {
        // Arrange
        IQueryable<TestEntity> entities = new TestEntity[]
        {
            TestEntity.Create("Not satisified").Value,

            TestEntity.Create("Test Specification 1").Value,
            TestEntity.Create("Test Specification 1").Value,

            TestEntity.Create("Test Specification 2").Value,
            TestEntity.Create("Test Specification 2").Value,
            TestEntity.Create("Test Specification 2").Value
        }
        .AsQueryable();

        // Act
        var spec1 = entities.Where(new TestSpecification1().ToExpression());
        var spec2 = entities.Where(new TestSpecification2().ToExpression());

        // Assert
        spec1.Should().HaveCount(2);
        spec2.Should().HaveCount(3);
    }
}