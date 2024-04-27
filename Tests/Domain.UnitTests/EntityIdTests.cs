using Domain.UnitTests.TestImplementations;

using FluentAssertions;

namespace Domain.UnitTests;
public class EntityIdTests
{
    [Fact]
    public void EntityId_ShouldImplicitlyConvertTo_Ulid()
    {
        // Arrange
        TestEntityId testId = new(Ulid.NewUlid());

        // Act
        Ulid converted = testId;

        // Assert
        converted.Should().Be(testId.Value);
    }

    [Fact]
    public void Ulid_ShouldExplicitlyConvertTo_EntityId()
    {
        // Arrange
        Ulid testId = Ulid.NewUlid();

        // Act
        TestEntityId converted = (TestEntityId)testId;

        // Assert
        converted.Should().Be(new TestEntityId(testId));
    }
}