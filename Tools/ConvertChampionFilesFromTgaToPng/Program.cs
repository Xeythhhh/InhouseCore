using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

string currentDirectory = Directory.GetCurrentDirectory();
string baseDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", ".."));
string assetsFolder = Path.Combine(baseDirectory, "Assets");
string battleriteFolder = Path.Combine(assetsFolder, "Assets");
if (!Directory.Exists(battleriteFolder))
{
    Console.WriteLine($"Battlerite folder not found at: {battleriteFolder}");
    return;
}

string championsFolder = Path.Combine(battleriteFolder, "Champions"); // do not include Champion.Original if you want to preserve the original .tga files
string[] tgaFiles = Directory.GetFiles(championsFolder, "*.tga", SearchOption.AllDirectories);

foreach (string tgaFile in tgaFiles)
{
    try
    {
        // Load the .tga file
        using (Image<Rgba32> image = Image.Load<Rgba32>(tgaFile))
        {
            // Replace the .tga extension with .png
            string pngFilePath = Path.ChangeExtension(tgaFile, ".png");

            // Save the image as .png in the same location
            image.SaveAsPng(pngFilePath);
            Console.WriteLine($"Converted: {tgaFile} -> {pngFilePath}");
        }

        // Delete the original .tga file
        File.Delete(tgaFile);
        Console.WriteLine($"Deleted original .tga file: {tgaFile}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to convert {tgaFile}: {ex.Message}");
    }
}

Console.WriteLine("Conversion complete.");