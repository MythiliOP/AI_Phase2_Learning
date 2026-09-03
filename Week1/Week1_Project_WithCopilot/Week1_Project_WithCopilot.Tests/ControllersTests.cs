using Microsoft.AspNetCore.Mvc;
using Moq;
using Week1_Project_WithCopilot.Controllers;
using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Services;
using Xunit;

namespace Week1_Project_WithCopilot.Tests;

public sealed class ControllersTests
{
    [Fact]
    public async Task Search_WithValidName_ReturnsCustomersAndUsesDefaultLimit()
    {
        // Arrange
        var customers = new[] { new Customer(1, "Alice Johnson", "alice@example.com") };
        var service = new Mock<ICustomerSearchService>();
        service
            .Setup(searchService => searchService.SearchAsync(
                "Alice",
                25,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);
        var controller = new CustomersController(service.Object);

        // Act
        var response = await controller.Search("Alice", null, CancellationToken.None);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Same(customers, result.Value);
        service.VerifyAll();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Search_WithoutName_ReturnsBadRequest(string? name)
    {
        // Arrange
        var service = new Mock<ICustomerSearchService>();
        var controller = new CustomersController(service.Object);

        // Act
        var response = await controller.Search(name, null, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
        service.Verify(
            searchService => searchService.SearchAsync(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task Search_WithLimitOutsideAllowedRange_ReturnsBadRequest(int limit)
    {
        // Arrange
        var service = new Mock<ICustomerSearchService>();
        var controller = new CustomersController(service.Object);

        // Act
        var response = await controller.Search("Alice", limit, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
        service.Verify(
            searchService => searchService.SearchAsync(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public async Task Search_WithBoundaryLimit_ForwardsLimit(int limit)
    {
        // Arrange
        var customers = Array.Empty<Customer>();
        var service = new Mock<ICustomerSearchService>();
        service
            .Setup(searchService => searchService.SearchAsync(
                "Alice",
                limit,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);
        var controller = new CustomersController(service.Object);

        // Act
        var response = await controller.Search("Alice", limit, CancellationToken.None);

        // Assert
        Assert.IsType<OkObjectResult>(response.Result);
        service.VerifyAll();
    }

    [Fact]
    public async Task Search_ForwardsCancellationToken()
    {
        // Arrange
        using var cancellationSource = new CancellationTokenSource();
        var service = new Mock<ICustomerSearchService>();
        service
            .Setup(searchService => searchService.SearchAsync(
                "Alice",
                25,
                cancellationSource.Token))
            .ReturnsAsync(Array.Empty<Customer>());
        var controller = new CustomersController(service.Object);

        // Act
        await controller.Search("Alice", null, cancellationSource.Token);

        // Assert
        service.VerifyAll();
    }

    [Fact]
    public async Task GetAll_ReturnsProducts()
    {
        // Arrange
        var products = new[] { new Product(1, "Laptop", 999.99m) };
        var service = new Mock<IProductService>();
        service
            .Setup(productService => productService.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        var controller = new ProductsController(service.Object);

        // Act
        var response = await controller.GetAll(CancellationToken.None);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Same(products, result.Value);
        service.Verify(
            productService => productService.GetAllAsync(CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task GetAll_ForwardsCancellationToken()
    {
        // Arrange
        using var cancellationSource = new CancellationTokenSource();
        var service = new Mock<IProductService>();
        service
            .Setup(productService => productService.GetAllAsync(cancellationSource.Token))
            .ReturnsAsync(Array.Empty<Product>());
        var controller = new ProductsController(service.Object);

        // Act
        await controller.GetAll(cancellationSource.Token);

        // Assert
        service.VerifyAll();
    }

    [Fact]
    public async Task Get_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var user = new User(1, "Alex Johnson", "alex@example.com");
        var service = new Mock<IUserService>();
        service
            .Setup(userService => userService.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        var controller = new UserController(service.Object);

        // Act
        var response = await controller.Get(CancellationToken.None);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response.Result);
        Assert.Same(user, result.Value);
        service.Verify(
            userService => userService.GetAsync(CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Get_WhenUserDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var service = new Mock<IUserService>();
        service
            .Setup(userService => userService.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);
        var controller = new UserController(service.Object);

        // Act
        var response = await controller.Get(CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(response.Result);
        service.Verify(
            userService => userService.GetAsync(CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Get_ForwardsCancellationToken()
    {
        // Arrange
        using var cancellationSource = new CancellationTokenSource();
        var service = new Mock<IUserService>();
        service
            .Setup(userService => userService.GetAsync(cancellationSource.Token))
            .ReturnsAsync((User?)null);
        var controller = new UserController(service.Object);

        // Act
        await controller.Get(cancellationSource.Token);

        // Assert
        service.VerifyAll();
    }
}
