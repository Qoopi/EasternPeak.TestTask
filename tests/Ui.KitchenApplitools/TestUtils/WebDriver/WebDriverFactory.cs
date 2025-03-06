using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Ui.KitchenApplitools.TestUtils.WebDriver;

public class WebDriverFactory : IWebDriverFactory
{
    private readonly ThreadLocal<IWebDriver> _threadLocalDriver = new();

    public IWebDriver Driver
    {
        get => _threadLocalDriver.Value!;
        set => _threadLocalDriver.Value = value;
    }

    public void OpenWith(BrowserOptions browser, params string[] args)
    {
        switch (browser)
        {
            case BrowserOptions.Chrome:
                var chromeOptions = new ChromeOptions();
                foreach (var arg in args) chromeOptions.AddArgument(arg);

                _threadLocalDriver.Value ??= new ChromeDriver(chromeOptions);
                break;
            case BrowserOptions.FireFox:
                var firefoxOptions = new FirefoxOptions();
                foreach (var arg in args) firefoxOptions.AddArgument(arg);

                _threadLocalDriver.Value ??= new FirefoxDriver(firefoxOptions);
                break;
            case BrowserOptions.Edge:
                var edgeOptions = new EdgeOptions();
                foreach (var arg in args) edgeOptions.AddArgument(arg);

                _threadLocalDriver.Value ??= new EdgeDriver(edgeOptions);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }
    }

    public void CloseBrowser()
    {
        if (_threadLocalDriver.Value == null) return;

        _threadLocalDriver.Value?.Quit();
        _threadLocalDriver.Value?.Dispose();
        _threadLocalDriver.Value = null;
    }
}