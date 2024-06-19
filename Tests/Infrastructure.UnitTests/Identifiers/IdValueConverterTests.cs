using Domain.UnitTests.TestImplementations;

using FluentAssertions;

using Infrastructure.Identifiers;

namespace Infrastructure.UnitTests.Identifiers;

public class IdValueConverterTestFixture
{
    public readonly int TestId = 69420;
}

public class IdValueConverterTests(IdValueConverterTestFixture fixture) :
    IClassFixture<IdValueConverterTestFixture>
{
    [Fact]
    public void ConvertToProvider_Success()
    {
        // Arrange
        IdValueConverter<TestEntityId, TestEntity> converter = new();

        // Act
        object? longValue = converter.ConvertToProvider(new TestEntityId(fixture.TestId));

        // Assert
        longValue.Should().Be(fixture.TestId);
    }

    [Fact]
    public void ConvertFromProvider_Success()
    {
        // Arrange
        IdValueConverter<TestEntityId, TestEntity> converter = new();

        // Act
        object? entityId = converter.ConvertFromProvider(fixture.TestId);

        // Assert
        entityId.Should().BeEquivalentTo(new TestEntityId(fixture.TestId));
    }
}
