using ApplicationServices;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Infrastructure;

public sealed class ServiceCollectionExtensionTests
{
    [Fact]
    public void GivenAddInfrastructureIsCalled_ThenShouldRegisterServices()
    {
        // Arrange
        var sut = new ServiceCollection();

        // Act
        _ = sut.AddInfrastructure(_ => { });

        // Assert
        _ = sut.FirstOrDefault(x => x.ServiceType == typeof(ITodoRepository)).Should().NotBeNull();
        _ = sut.FirstOrDefault(x => x.ServiceType == typeof(TodoContext)).Should().NotBeNull();
        _ = sut.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions)).Should().NotBeNull();
        _ = sut.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<TodoContext>)).Should().NotBeNull();
    }

    [Fact]
    public void GivenAddInfrastructureIsCalled_ThenShouldRegisterAllServices()
    {
        // Arrange
        var sut = new ServiceCollection();

        // Act
        _ = sut.AddInfrastructure(_ => { });

        // Assert
        _ = sut.Count.Should().Be(4);
    }
}
