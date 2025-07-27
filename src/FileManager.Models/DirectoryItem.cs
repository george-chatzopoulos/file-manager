namespace FileManager.Models;

using System.Collections.Generic;
using System.Linq;

using FileManager.Common;


public sealed class DirectoryItem
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = "/";

    public DirectoryItem(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (!Models.IsDirectoryNameValid(name))
            throw new ArgumentException("Invalid directory name", nameof(name));

        Name = name;
    }
}
