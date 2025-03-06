using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using SeleniumExtras.WaitHelpers;

namespace Ui.KitchenApplitools.Pages;

/// <summary>
/// Base page class with common functionality for all pages
/// </summary>
public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    protected readonly ILogger Logger;
    protected const string BaseUrl = "https://kitchen.applitools.com/";
    
    protected BasePage(IWebDriver driver, ILogger logger, int timeoutInSeconds = 10)
    {
        Driver = driver;
        Logger = logger;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
    }
    
    /// <summary>
    /// Navigate to a specific URL
    /// </summary>
    /// <param name="url">URL to navigate to</param>
    public void NavigateTo(string url)
    {
        Logger.Information($"Navigating to {url}");
        Driver.Navigate().GoToUrl(url);
    }
    
    /// <summary>
    /// Wait for an element to be visible
    /// </summary>
    /// <param name="locator">Element locator</param>
    /// <returns>The visible web element</returns>
    protected IWebElement WaitForElementVisible(By locator)
    {
        Logger.Information($"Waiting for element to be visible: {locator}");
        return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }
    
    /// <summary>
    /// Wait for an element to be clickable
    /// </summary>
    /// <param name="locator">Element locator</param>
    /// <returns>The clickable web element</returns>
    protected IWebElement WaitForElementClickable(By locator)
    {
        Logger.Information($"Waiting for element to be clickable: {locator}");
        return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }
    
    /// <summary>
    /// Execute JavaScript
    /// </summary>
    /// <param name="script">JavaScript to execute</param>
    /// <param name="args">Arguments to pass to the script</param>
    /// <returns>The result of the script execution</returns>
    protected object ExecuteJavaScript(string script, params object[] args)
    {
        return ((IJavaScriptExecutor)Driver).ExecuteScript(script, args);
    }
} 