﻿string currentDirectory = Directory.GetCurrentDirectory();
string baseDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", ".."));
string assetsFolder = Path.Combine(baseDirectory, "Assets");
if (!Directory.Exists(assetsFolder))
{
    Console.WriteLine($"Assets folder not found at: {assetsFolder}");
    return;
}

string championsFolder = Path.Combine(assetsFolder, "Champions");

Dictionary<string, List<string>> championMissingFiles = new();

List<string> requiredFiles = new()
{
    "meta_portrait",
    "meta_wide",
    "m1_",
    "m2_",
    "q_",
    "e_",
    "r_",
    "f_",
    "space_"
};

List<string> optionalFiles = new()
{
    "talent_"
};

int requiredExFilesCount = 2;

Dictionary<string, List<string>> missingFilesReport = new();
Dictionary<string, List<string>> existingOtionalFilesReport = new();

foreach (string championDir in Directory.GetDirectories(championsFolder))
{
    string championName = Path.GetFileName(championDir);
    string missingFilePath = Path.Combine(championDir, "missing.txt");

    if (File.Exists(missingFilePath))
    {
        string[] missingFiles = File.ReadAllLines(missingFilePath);

        championMissingFiles[championName] = new List<string>(missingFiles);
    }

    List<string> missingSpecificFiles = new();
    List<string> existingOptionalFiles = new();

    foreach (string requiredFile in requiredFiles)
    {
        if (!FileExistsWithPrefix(championDir, requiredFile))
        {
            missingSpecificFiles.Add(requiredFile);
        }
    }

    int exFilesCount = CountFilesWithPrefix(championDir, "ex");
    if (exFilesCount < requiredExFilesCount)
    {
        missingSpecificFiles.Add($"ex_ (found {exFilesCount}/2)");
    }

    foreach (string optionalFile in optionalFiles)
    {
        if (FileExistsWithPrefix(championDir, optionalFile))
        {
            existingOptionalFiles.Add($"[Optional] {optionalFile}");
        }
    }

    if (missingSpecificFiles.Count > 0)
    {
        missingFilesReport[championName] = missingSpecificFiles;
    }

    if (existingOptionalFiles.Count > 0)
    {
        existingOtionalFilesReport[championName] = existingOptionalFiles;
    }
}

Console.WriteLine("\nMissing Files Report:\n");
foreach (KeyValuePair<string, List<string>> champion in championMissingFiles)
{
    Console.WriteLine($"\n{champion.Key}:");
    foreach (string missingFile in champion.Value)
    {
        Console.WriteLine($" - {missingFile}");
    }
}

foreach (KeyValuePair<string, List<string>> entry in missingFilesReport)
{
    Console.WriteLine($"Champion: {entry.Key}");
    foreach (string missingFile in entry.Value)
    {
        Console.WriteLine($" - {missingFile}");
    }
}

Console.WriteLine("\nExisting Optional Files Report:\n");
foreach (KeyValuePair<string, List<string>> entry in existingOtionalFilesReport)
{
    Console.WriteLine($"Champion: {entry.Key}");
    foreach (string existingFile in entry.Value)
    {
        Console.WriteLine($" - {existingFile}");
    }
}

Console.WriteLine("\nFile check complete.");

static bool FileExistsWithPrefix(string directory, string prefix) =>
    Directory.GetFiles(directory, $"{prefix}*").Length > 0;

static int CountFilesWithPrefix(string directory, string prefix) =>
    Directory.GetFiles(directory, $"{prefix}*").Length;