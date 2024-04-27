using Domain.Entities.Users;
using Domain.UnitTests.TestImplementations;

using FluentAssertions;

namespace Domain.UnitTests;
public class EntityIdTests
{
    [Fact]
    public void EntityId_ShouldImplicitlyConvertTo_Ulid()
    {
        // Arrange
        ApplicationUserId testId = new(Ulid.NewUlid());

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
        ApplicationUserId converted = (ApplicationUserId)testId;

        // Assert
        converted.Should().Be(new ApplicationUserId(testId));
    }
}