using System;

using Domain.Users;

using FluentAssertions;

using Xunit;

namespace Domain.UnitTests.Users;
public class ApplicationUserTests
{
    [Fact]
    public void Activator_CanCreate_ApplicationUser()
    {
        ApplicationUser user = Activator.CreateInstance<ApplicationUser>();

        user.Should().NotBeNull();
    }
}
