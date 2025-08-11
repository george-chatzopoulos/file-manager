namespace FileManager.Models;

using System.Data;

using FileManager.Common;


public sealed class DirectoryItem
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = "/";
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModifiedAt { get; set; }

    public List<FileItem> FileItems { get; set; }
    public List<DirectoryItem> DirectoryItems { get; set; }

    public DirectoryItem(string name, string path = "/")
    {
        SetName(name);
        SetPath(path);
        CreatedAt = DateTime.Now;
        LastModifiedAt = DateTime.Now;
    }

    public void SetName(string name)
    {
        if (!Models.IsDirectoryNameValid(name))
            throw new ArgumentException("Invalid directory name", nameof(name));

        Name = name;
        LastModifiedAt = DateTime.Now;
    }

    public void SetPath(string path)
    {
        if (!Models.IsPathValid(path))
            throw new ArgumentException($"Invalid path", nameof(path));

        Path = path + Name;
        LastModifiedAt = DateTime.Now;
    }
}
