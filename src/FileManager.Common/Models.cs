using System.Text.RegularExpressions;

namespace FileManager.Common;

public static class Models
{
    public static bool IsDirectoryNameValid(string name)
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
        string pattern = @"[a-zA-Z][a-zA-Z0-9-_\.]*$";
        bool hasNameValidPattern = Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);

        return isNameNotEmpty && hasNameValidPattern;
    }

    public static bool IsFileNameValid(string name)
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
        string pattern = @"^[\w\s-]+(\.[\w\.]+[\w]+)$";
        bool hasNameValidPattern = Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);

        return isNameNotEmpty && hasNameValidPattern;
    }
}
