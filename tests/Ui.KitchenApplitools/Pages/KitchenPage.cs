using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Serilog;
using Ui.KitchenApplitools.TestUtils.Helpers;
using Ui.KitchenApplitools.TestUtils.Screenshots;

namespace Ui.KitchenApplitools.Pages;

/// <summary>
/// Page object for Kitchen Applitools page
/// </summary>
public class KitchenPage : BasePage
{
    private const string DragAndDropUrl = "https://kitchen.applitools.com/ingredients/drag-and-drop";
    private readonly IScreenshotComparer _screenshotComparer;

    // Locators
    private readonly By _menuItemsLocator = By.XPath("//ul[@id='menu-items']/*");
    private readonly By _plate = By.Id("plate-items");
    private readonly By _plateItems = By.XPath("//ul[@id='plate-items']/*");

    public KitchenPage(IWebDriver driver, ILogger logger) : base(driver, logger)
    {
        _screenshotComparer = new ScreenshotComparer(logger);
    }

    /// <summary>
    /// Navigate to the Kitchen Applitools homepage
    /// </summary>
    public void NavigateToHomePage()
    {
        Logger.Information("Navigating to Kitchen Applitools homepage");
        NavigateTo(BaseUrl);
    }

    /// <summary>
    /// Navigate to the Drag and Drop section
    /// </summary>
    public void NavigateToDragAndDropSection()
    {
        Logger.Information("Navigating to Drag and Drop section");
        NavigateTo(DragAndDropUrl);
        WaitForElementVisible(_plateItems);
    }

    /// <summary>
    /// Drag and drop a menu item to the order ticket using standard Actions
    /// </summary>
    /// <param name="menuItemText">Text of the menu item to drag</param>
    public void DragMenuItemToOrderTicket(string menuItemText)
    {
        Logger.Information($"Dragging menu item '{menuItemText}' to order ticket");

        // Find the menu item by text
        var menuItem = FindMenuItemByText(menuItemText);

        // Make sure that the order ticket is visible
        var orderTicket = WaitForElementVisible(_plate);

        // Perform drag and drop using Actions
        var actions = new Actions(Driver);
        actions.DragAndDrop(menuItem, orderTicket).Perform();

        Logger.Information($"Menu item '{menuItemText}' dragged to order ticket");
    }

    /// <summary>
    /// Drag and drop a menu item to the order ticket using JavaScript
    /// Use this method if standard Actions drag and drop doesn't work
    /// </summary>
    /// <param name="menuItemText">Text of the menu item to drag</param>
    public void DragMenuItemToOrderTicketWithJavaScript(string menuItemText)
    {
        Logger.Information($"Dragging menu item '{menuItemText}' to order ticket using JavaScript");

        // Find the menu item by text
        var menuItem = FindMenuItemByText(menuItemText);

        // Make sure that the order ticket is visible
        var orderTicket = WaitForElementVisible(_plate);

        // JavaScript drag and drop implementation
        var dragAndDropScript = PathHelper.GetJsScript("drag_and_drop.js");
        ExecuteJavaScript(dragAndDropScript, menuItem, orderTicket);

        Logger.Information($"Menu item '{menuItemText}' dragged to order ticket using JavaScript");
    }

    /// <summary>
    /// Get the text from the order ticket
    /// </summary>
    public string GetOrderTicketText()
    {
        var orderTicket = WaitForElementVisible(_plateItems);
        var text = orderTicket.Text;
        Logger.Information($"Order ticket text: '{text}'");
        return text;
    }

    /// <summary>
    /// Get count of items in the order ticket
    /// </summary>
    public int GetOrderItemCount()
    {
        var orderTicket = WaitForElementVisible(_plate);
        return orderTicket.FindElements(_plateItems).Count;
    }

    /// <summary>
    ///  Compare the screenshot of the drop area with the expected image
    /// </summary>
    public async Task<bool> VerifyDropAreaVisually(string menuItemText)
    {
        Logger.Information($"Performing visual validation of drop area after dropping '{menuItemText}'");

        WaitForElementVisible(_plate);

        // Capture screenshot of drop area and get full path to it
        var filePath = CaptureScreenshot();

        // Compare the screenshot with the expected image
        return await _screenshotComparer.Compare(filePath);
    }

    private string CaptureScreenshot()
    {
        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        var filePath = PathHelper.GetFullPathToScreenshot();

        if (File.Exists(filePath))
            File.Delete(filePath);

        Logger.Information($"Saving screenshot to: {filePath}");
        screenshot.SaveAsFile(filePath);
        return filePath;
    }

    private IWebElement FindMenuItemByText(string menuItemText)
    {
        var menuItems = Driver.FindElements(_menuItemsLocator);
        Logger.Information($"Found {menuItems.Count} menu items");

        var menuItem = menuItems.FirstOrDefault(item =>
            item.Text.Equals(menuItemText, StringComparison.OrdinalIgnoreCase));

        if (menuItem == null)
        {
            Logger.Error($"Menu item '{menuItemText}' not found");
            throw new NotFoundException($"Menu item '{menuItemText}' not found");
        }

        return menuItem;
    }
}