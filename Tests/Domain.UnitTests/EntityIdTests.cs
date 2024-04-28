using Domain.Entities.Users;

using FluentAssertions;

using IdGen;

namespace Domain.UnitTests;
public class EntityIdTests
{
    [Fact]
    public void EntityId_ShouldImplicitlyConvertTo_Int64()
    {
        // Arrange
        IdGenerator idGenerator = new(69);
        ApplicationUserId testId = new(idGenerator.CreateId());

        // Act
        long converted = testId;

        // Assert
        converted.Should().Be(testId.Value);
    }

    [Fact]
    public void Int64_ShouldExplicitlyConvertTo_EntityId()
    {
        // Arrange
        IdGenerator idGenerator = new(69);
        long testId = idGenerator.CreateId();

        // Act
        ApplicationUserId converted = (ApplicationUserId)testId;

        // Assert
        converted.Should().Be(new ApplicationUserId(testId));
    }
}