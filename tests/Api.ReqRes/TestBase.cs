using Api.ReqRes.Clients;
using Serilog;

namespace Api.ReqRes;


public abstract class TestBase
{
    protected IReqResApiClient ApiClient;
    protected ILogger Logger;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize logger
        Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        Logger.Information("Starting ReqRes API tests");

        // Initialize API client
        ApiClient = new ReqResApiClient(Logger);
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Logger.Information("Completed ReqRes API tests");

        // Dispose logger if it implements IDisposable
        (Logger as IDisposable)?.Dispose();
    }
    
}