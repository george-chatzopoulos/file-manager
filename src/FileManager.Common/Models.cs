using System.Text.RegularExpressions;

namespace FileManager.Common;

public static partial class Models
{
    [GeneratedRegex(@"^[a-zA-Z][a-zA-Z0-9-_\.]*$", RegexOptions.IgnoreCase)]
    private static partial Regex DirectoryNamePattern();

    [GeneratedRegex(@"^[\w\s-]+(\.[\w\.]+[\w]+)$", RegexOptions.IgnoreCase)]
    private static partial Regex FileNamePattern();

    [GeneratedRegex(@"^(\/\w)+$", RegexOptions.IgnoreCase)]
    private static partial Regex IsPathValid();

    public static bool IsDirectoryNameValid(string name)
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
        bool hasNameValidPattern = DirectoryNamePattern().IsMatch(name);

        return isNameNotEmpty && hasNameValidPattern;
    }

    public static bool IsFileNameValid(string name)
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
        bool hasNameValidPattern = FileNamePattern().IsMatch(name);

        return isNameNotEmpty && hasNameValidPattern;
    }

    public static bool IsPathValid(string path)
    {
        bool isPathNotEmpty = !string.IsNullOrWhiteSpace(path);
        bool hasPathValidPattern = IsPathValid().IsMatch(path);

        return isPathNotEmpty && hasPathValidPattern;
    }
}
