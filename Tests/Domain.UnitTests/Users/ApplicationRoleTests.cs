//using Domain.Users;

//using FluentAssertions;

//using SharedKernel.Primitives.Result;

//namespace Domain.UnitTests.Users;
//public class ApplicationRoleTests
//{
//    [Fact]
//    public void Create_ValidName_ShouldReturnSuccess()
//    {
//        // Arrange
//        const string validName = "Admin";

//        // Act
//        Result<ApplicationRole> result = ApplicationRole.Create(validName);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Should().NotBeNull();
//        result.Value.Name.Should().Be(validName);
//    }

//    [Fact]
//    public void Create_EmptyName_ShouldReturnFailure()
//    {
//        // Arrange
//        string invalidName = string.Empty;

//        // Act
//        Result<ApplicationRole> result = ApplicationRole.Create(invalidName);

//        // Assert
//        result.IsFailed.Should().BeTrue();
//        result.HasException<ArgumentException>();
//    }

//    [Fact]
//    public void Create_NullName_ShouldReturnFailure()
//    {
//        // Arrange
//        const string? invalidName = null;

//        // Act
//        Result<ApplicationRole> result = ApplicationRole.Create(invalidName!);

//        // Assert
//        result.IsFailed.Should().BeTrue();
//        result.HasException<ArgumentNullException>();
//    }
//}