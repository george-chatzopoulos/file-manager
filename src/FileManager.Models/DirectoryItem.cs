namespace FileManager.Models;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


public sealed class DirectoryItem : IDisposable
{
    public string Name { get; set; }
    public Dictionary<string, FileItem>? Files;

    public DirectoryItem(string name, Dictionary<string, FileItem>? files = null)
    {
        SetName(name);
        Files = files ?? new Dictionary<string, FileItem>();
    }

    public void SetName(string name)
    {
        if (!IsNameValid(name))
            throw new ArgumentException("Invalid directory name", nameof(name));
        if (ContainsFile(name))
            throw new ArgumentException("File already exists", nameof(name));

        Name = name;
    }

    private bool IsNameValid(string name)
    {
        bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
        string pattern = @"[a-zA-Z][a-zA-Z0-9-_\.]*$";
        bool hasNameValidPattern = Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);

        return isNameNotEmpty && hasNameValidPattern;
    }

    public FileItem? GetFileItem(string name)
    {
        Files ??= new Dictionary<string, FileItem>();
        return Files.TryGetValue(name, out var file) ? file : null;
    }

    public void UpdateFileItem(string name, FileItem file)
    {
        Files ??= new Dictionary<string, FileItem>();
        Files[name] = file;
    }

    public void AddFile(FileItem file)
    {
        Files ??= new Dictionary<string, FileItem>();
        Files.Add(file.Name, file);
    }

    public bool RemoveFile(string name)
    {
        return Files?.Remove(name) ?? false;
    }

    public bool ContainsFile(string name)
    {
        return Files?.ContainsKey(name) ?? false;
    }

    public IEnumerable<FileItem> GetAllFIles()
    {
        return Files?.Values ?? Enumerable.Empty<FileItem>();
    }


    public void Empty()
    {
        Files = null;
    }


    public void Dispose()
    {
        if (Files != null)
        {
            foreach (var file in Files.Values)
            {
                file?.Dispose();
            }
            Files = null;
        }
    }
}