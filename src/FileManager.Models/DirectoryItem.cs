namespace FileManager.Models;

using FileManager.Common;


public sealed class DirectoryItem
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = "/";

    public DirectoryItem(string name, string path = "/")
    {
        SetName(name);
        SetPath(path);
    }

    public void SetName(string name)
    {
        if (!Models.IsDirectoryNameValid(name))
            throw new ArgumentException("Invalid directory name", nameof(name));

        Name = name;
    }

    public void SetPath(string path)
    {
        if (!Models.IsPathValid(path))
            throw new ArgumentException($"Invalid path", nameof(path));

        Path = path + Name;
    }
}
