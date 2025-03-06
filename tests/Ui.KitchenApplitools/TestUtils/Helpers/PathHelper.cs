namespace Ui.KitchenApplitools.TestUtils.Helpers;

public static class PathHelper
{
    private static readonly string ResourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
    private static readonly string ScreenshotsPath = Path.Combine(ResourcesPath, "Screenshots");
    private static readonly string ToolsPath = Path.Combine(Directory.GetCurrentDirectory(), "TestUtils");
    private static readonly string ScriptsPath = Path.Combine(ToolsPath, "Scripts");

    public const string ScreenshotFileName = "Screenshot";
    public const string FileExtension = ".png";

    public static string GetJsScript(string scriptName)
        => File.ReadAllText(Path.Combine(ScriptsPath, scriptName));


    public static string GetFullPathToScreenshot()
    {
        var directoryPath = Path.Combine(ScreenshotsPath, GetTestName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var fullPath = Path.Combine(directoryPath, ScreenshotFileName + FileExtension);
        return fullPath;
    }

    public static string GetScreenshotsDirectory()
    {
        return Path.Combine(ScreenshotsPath, GetTestName);
        ;
    }

    private static string GetTestName => TestContext.CurrentContext.Test.Name
        .Replace("(", "_")
        .Replace(" ", "_")
        .Replace(")", string.Empty)
        .Replace("\"", string.Empty);
}