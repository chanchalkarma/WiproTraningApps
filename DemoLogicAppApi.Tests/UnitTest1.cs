using DemoAngularCrudApi.Controllers;
using DemoAngularCrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoLogicAppApi.Tests;

public class CustomersControllerTests
{
    [Fact]
    public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        await using var dbContext = CreateDbContext();
        var controller = new CustomersController(dbContext);

        var result = await controller.GetCustomer(404);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostCustomer_CreatesCustomer()
    {
        await using var dbContext = CreateDbContext();
        var controller = new CustomersController(dbContext);
        var input = new Customer
        {
            Id = 1,
            Name = "Ada",
            Address = "London",
            PhoneNo = "1234567890",
            Amt = 250
        };

        var result = await controller.PostCustomer(input);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdCustomer = Assert.IsType<Customer>(createdResult.Value);

        Assert.Equal(1, createdCustomer.Id);
        Assert.Equal("Ada", createdCustomer.Name);
    }

    private static AngCustDBContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AngCustDBContext>()
            .UseInMemoryDatabase($"customers-tests-{Guid.NewGuid()}")
            .Options;

        return new AngCustDBContext(options);
    }
}
