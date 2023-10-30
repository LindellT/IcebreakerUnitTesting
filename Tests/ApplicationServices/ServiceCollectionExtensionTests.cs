using ApplicationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.ApplicationServices;

public sealed class ServiceCollectionExtensionTests
{
    [Fact]
    public void GivenAddApplicationServicesIsCalled_ThenShouldRegisterServices()
    {
        // Arrange
        var sut = new ServiceCollection();

        // Act
        _ = sut.AddApplicationServices();

        // Assert
        _ = sut.FirstOrDefault(x => x.ServiceType == typeof(ITodoService)).Should().NotBeNull();
    }

    [Fact]
    public void GivenAddApplicationServicesIsCalled_ThenShouldRegisterAllServices()
    {
        // Arrange
        var sut = new ServiceCollection();

        // Act
        _ = sut.AddApplicationServices();

        // Assert
        _ = sut.Count.Should().Be(1);
    }
}
