using FluentAssertions;

namespace Domain.UnitTests;

public class EntityIdTests
{
    [Fact]
    public void EntityId_ShouldImplicitlyConvertTo_Ulid()
    {
        // Arrange
        var testId = new TestEntityId(Ulid.NewUlid());

        // Act
        Ulid converted = testId;

        // Assert
        converted.Should().Be(testId.Value);
    }
}