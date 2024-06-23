using Domain.UnitTests.TestImplementations;

using FluentAssertions;

using IdGen;

namespace Domain.UnitTests.Primitives
{
    public class EntityBaseTests
    {
        private static readonly IdGenerator _idGenerator = new(69);

        [Fact]
        public void Constructor_ShouldInitialize_Dates()
        {
            // Arrange & Act
            TestEntity entity = TestEntity.Create().Value;

            // Assert
            entity.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            entity.LastUpdatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void EntityId_ShouldImplicitlyConvertTo_Int64()
        {
            // Arrange
            TestEntityId testId = new(_idGenerator.CreateId());

            // Act
            long converted = testId;

            // Assert
            converted.Should().Be(testId.Value);
        }

        [Fact]
        public void Int64_ShouldExplicitlyConvertTo_EntityId()
        {
            // Arrange
            long testId = _idGenerator.CreateId();

            // Act
            TestEntityId converted = (TestEntityId)testId;

            // Assert
            converted.Should().Be(new TestEntityId(testId));
        }
    }
}