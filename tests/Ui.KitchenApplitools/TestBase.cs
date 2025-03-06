using Serilog;
using Ui.KitchenApplitools.TestUtils.WebDriver;

namespace Ui.KitchenApplitools;

public abstract class TestBase
{
    protected IWebDriverFactory DriverFactory;
    protected ILogger Logger;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize logger
        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        // Initialize Verify
        VerifyImageSharpCompare.RegisterComparer(1, "png");
        VerifyImageSharpCompare.Initialize();

        // Initialize WebDriverFactory
        DriverFactory = new WebDriverFactory();

        Logger.Information("Starting Kitchen Applitools UI tests");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Logger.Information("Completed Kitchen Applitools UI tests");

        // Dispose logger if it implements IDisposable
        (Logger as IDisposable)?.Dispose();
    }
}