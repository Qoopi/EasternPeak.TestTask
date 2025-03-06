using Serilog;
using Ui.KitchenApplitools.TestUtils.Helpers;

namespace Ui.KitchenApplitools.TestUtils.Screenshots;

public class ScreenshotComparer : IScreenshotComparer
{
    private readonly ILogger _logger;


    public ScreenshotComparer(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<bool> Compare(string filePath)
    {
        try
        {
            // Use Verify to check the image
            var settings = new VerifySettings();
                settings.UseDirectory(PathHelper.GetScreenshotsDirectory());
                settings.UseFileName(PathHelper.ScreenshotFileName);
                
            _logger.Information($"Comparing screenshot at: {filePath}");

            if (!File.Exists(filePath))
            {
                _logger.Error($"Screenshot file not found: {filePath}");
                return false;
            }
            
            await VerifyFile(filePath, settings);

            _logger.Information($"Visual validation successful for menu item");

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Visual validation failed for menu item with error:\n{ex.Message}");
            return false;
        }
    }
}