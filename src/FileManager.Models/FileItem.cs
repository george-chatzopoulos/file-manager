namespace FileManager.Models;

using FileManager.Common;

public sealed class FileItem
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Path { get; private set; } = "/";
    public long Size { get; set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModifiedAt { get; set; }
    public int? CurrentDirectoryId { get; set; }


    public FileItem(string name, string path = "/", int? currentDirectoryId = null)
    {
        SetName(name);
        SetPath(path);
        CreatedAt = DateTime.Now;
        LastModifiedAt = DateTime.Now;
        CurrentDirectoryId = currentDirectoryId;
    }

    public void SetName(string name)
    {
        if (!Models.IsFileNameValid(name))
            throw new ArgumentException("Invalid file name", nameof(name));

        Name = name;
        Extension = System.IO.Path.GetExtension(name);
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
