using System.Text.RegularExpressions;

namespace FileManager.Common;

public static partial class Models
{
    [GeneratedRegex(@"^[a-zA-Z][a-zA-Z0-9-_\.]*\/$", RegexOptions.IgnoreCase)]
    private static partial Regex DirectoryNamePattern();

    [GeneratedRegex(@"^[\w\s-]+(\.[\w\.]+[\w]+)$", RegexOptions.IgnoreCase)]
    private static partial Regex FileNamePattern();

    [GeneratedRegex(@"^(\/[a-zA-Z][a-zA-Z0-9-_\.]*)+\/$", RegexOptions.IgnoreCase)]
    private static partial Regex PathPattern();

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

    public static bool IsPathRoot(string path)
    {
        return path.Equals("/");
    }

    public static bool IsPathValid(string path)
    {
        bool hasPathValidPattern = PathPattern().IsMatch(path);

        return IsPathRoot(path) || hasPathValidPattern;
    }

    public static bool IsThereARootDirectory()
    {
        var root = async 
    }
}
