using Api.ReqRes.Clients;
using Api.ReqRes.Models;
using Serilog;

namespace Api.ReqRes;

[TestFixture]
public class ReqResTests : TestBase
{
    [SetUp]
    public void SetUp()
    {
        Logger.Information($"Setting up test:{TestContext.CurrentContext.Test.Name}");
    }


    [Test]
    public async Task GetUserList_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await ApiClient.GetUserListAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Data, Is.Not.Empty, "User data list should not be empty");
            Assert.That(response.Data[0].Id, Is.GreaterThan(0), "First user ID should not be empty");
        });
    }

    [Test]
    public async Task CreateUser_ReturnsCreatedStatusWithValidData()
    {
        // Arrange
        var createRequest = new CreateUserRequest
        {
            Name = "John Doe",
            Job = "Software Engineer"
        };

        // Act
        var response = await ApiClient.CreateUserAsync(createRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Id, Is.Not.Empty, "ID should not be empty");
            Assert.That(response.Name, Is.EqualTo(createRequest.Name), "Name in response should match request");
            Assert.That(response.Job, Is.EqualTo(createRequest.Job), "Job in response should match request");
        });
    }

    [TearDown]
    public void TearDown()
    {
        Logger.Information($"Tearing down test:{TestContext.CurrentContext.Test.Name}");
    }
}