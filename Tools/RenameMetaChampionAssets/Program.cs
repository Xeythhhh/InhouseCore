string currentDirectory = Directory.GetCurrentDirectory();
string baseDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", ".."));
string assetsFolder = Path.Combine(baseDirectory, "Assets");
string battleriteFolder = Path.Combine(assetsFolder, "Game-Battlerite");
if (!Directory.Exists(battleriteFolder))
{
    Console.WriteLine($"Battlerite folder not found at: {battleriteFolder}");
    return;
}

string championsFolder = Path.Combine(battleriteFolder, "Champions");

List<string> requiredFiles = new()
{
    "meta_portrait",
    "meta_wide"
};

Dictionary<string, List<string>> metaFilesReport = new();

foreach (string championDir in Directory.GetDirectories(championsFolder))
{
    string championName = Path.GetFileName(championDir);
    List<string> renamedFiles = new();

    foreach (string requiredFile in requiredFiles)
    {
        string originalFilePath = Path.Combine(championDir, $"{requiredFile}.png");
        if (File.Exists(originalFilePath))
        {
            string newFileName = requiredFile.Replace("meta_", "") + ".png";
            string newFilePath = Path.Combine(championDir, newFileName);

            // Rename the file
            File.Move(originalFilePath, newFilePath);

            // Add the new file name to the report
            renamedFiles.Add(newFileName);
        }
    }

    if (renamedFiles.Count > 0)
    {
        metaFilesReport[championName] = renamedFiles;
    }
}

foreach (KeyValuePair<string, List<string>> entry in metaFilesReport)
{
    Console.WriteLine($"Champion: {entry.Key}");
    foreach (string file in entry.Value)
    {
        Console.WriteLine($" - Renamed {file}");
    }
}
