using Serilog;
using Ui.KitchenApplitools.Pages;
using Ui.KitchenApplitools.TestUtils.WebDriver;
using static Ui.KitchenApplitools.TestUtils.WebDriver.BrowserOptions;

namespace Ui.KitchenApplitools;

[TestFixture]
public class DragDropTests : TestBase
{
    private KitchenPage _kitchenPage;


    [SetUp]
    public void SetUp()
    {
        Logger.Information($"Setting up test:{TestContext.CurrentContext.Test.Name}");

        // Open browser
        DriverFactory.OpenWith(Chrome, "--start-maximized", "--headless");

        // Initialize page objects
        _kitchenPage = new KitchenPage(DriverFactory.Driver, Logger);
    }


    [Test]
    [TestCase("Fried Chicken")]
    [TestCase("Hamburger")]
    [TestCase("Ice Cream")]
    public async Task DragAndDrop_StandardActions_OrderTicketContainsItem(string menuItem)
    {
        // Arrange
        _kitchenPage.NavigateToHomePage();
        _kitchenPage.NavigateToDragAndDropSection();

        // Act
        _kitchenPage.DragMenuItemToOrderTicket(menuItem);

        // Assert
        var itemsInOrderTicket = _kitchenPage.GetOrderItemCount();
        var itemText = _kitchenPage.GetOrderTicketText();
        var visuallyVerified = await _kitchenPage.VerifyDropAreaVisually(menuItem);

        Assert.Multiple(() =>
        {
            Assert.That(itemsInOrderTicket, Is.EqualTo(1), "Order ticket should contain 1 item");
            Assert.That(itemText, Is.EqualTo(menuItem), $"Order ticket should contain the text '{menuItem}'");
            Assert.That(visuallyVerified, Is.True, "Visual verification failed");
        });
    }

    [Test]
    [TestCase("Fried Chicken")]
    [TestCase("Hamburger")]
    [TestCase("Ice Cream")]
    public async Task DragAndDrop_JavaScriptExecutor_OrderTicketContainsItem(string menuItem)
    {
        // Arrange
        _kitchenPage.NavigateToHomePage();
        _kitchenPage.NavigateToDragAndDropSection();

        // Act
        _kitchenPage.DragMenuItemToOrderTicketWithJavaScript(menuItem);

        // Assert
        var itemsInOrderTicket = _kitchenPage.GetOrderItemCount();
        var itemText = _kitchenPage.GetOrderTicketText();
        var visuallyVerified = await _kitchenPage.VerifyDropAreaVisually(menuItem);

        Assert.Multiple(() =>
        {
            Assert.That(itemsInOrderTicket, Is.EqualTo(1), "Order ticket should contain 1 item");
            Assert.That(itemText, Is.EqualTo(menuItem), $"Order ticket should contain the text '{menuItem}'");
            Assert.That(visuallyVerified, Is.True, "Visual verification failed");
        });
    }


    [TearDown]
    public void TearDown()
    {
        // Log the finish of the test
        Logger.Information($"Tearing down test:{TestContext.CurrentContext.Test.Name}");

        DriverFactory.CloseBrowser();
    }
}