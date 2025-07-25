namespace FileManager.Models;

using System.Collections.Generic;
using System.Linq;

using FileManager.Common;


public sealed class DirectoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    private Dictionary<string, FileItem> _files = [];

    public DirectoryItem(string name, Dictionary<string, FileItem>? files = null)
    {
        SetName(name);
        _files = new Dictionary<string, FileItem>(files ?? []);
    }

    public void SetName(string name)
    {
        if (!Models.IsDirectoryNameValid(name))
            throw new ArgumentException("Invalid directory name", nameof(name));

        Name = name;
    }

    public FileItem? GetFile(string name)
    {
        return _files.TryGetValue(name, out var file) ? file : null;
    }

    public void AddOrUpdateFile(FileItem file)
    {
        _files[file.Name] = file;
    }

    public bool RemoveFile(string name)
    {
        return _files?.Remove(name) ?? false;
    }

    public bool ContainsFile(string name)
    {
        return _files?.ContainsKey(name) ?? false;
    }

    public IEnumerable<FileItem> GetAllFiles()
    {
        return _files?.Values ?? Enumerable.Empty<FileItem>();
    }


    public void Clear()
    {
        _files.Clear();
    }
}
