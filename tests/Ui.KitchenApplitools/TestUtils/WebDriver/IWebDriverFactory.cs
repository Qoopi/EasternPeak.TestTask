using OpenQA.Selenium;

namespace Ui.KitchenApplitools.TestUtils.WebDriver;

public interface IWebDriverFactory
{
    IWebDriver Driver { get; set; }
    void OpenWith(BrowserOptions browser, params string[] args);
    void CloseBrowser();
}