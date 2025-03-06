namespace Ui.KitchenApplitools.TestUtils.Screenshots;

public interface IScreenshotComparer
{
    Task<bool> Compare(string filePath);
}