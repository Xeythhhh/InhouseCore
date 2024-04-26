using Domain.UnitTests.Entities;

using FluentAssertions;

namespace Domain.UnitTests;

public class EntityIdTests
{
    [Fact]
    public void EntityId_ShouldImplicitlyConvertTo_Ulid()
    {
        // Arrange
        DateTime utcNow = DateTime.UtcNow;
        var testId = new TestEntityId(Ulid.NewUlid(utcNow));

        // Act
        Ulid actual = testId;

        // Assert
        actual.Should().Be(Ulid.NewUlid(utcNow));
    }
}