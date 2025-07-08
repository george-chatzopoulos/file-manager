namespace FileManager.Models;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;


public sealed class DirectoryItem : IDisposable
{
    public string Name { get; set; }
    private Dictionary<string, FileItem> _files = [];

    public DirectoryItem(string name, Dictionary<string, FileItem> files)
    {
        SetName(name);
        _files = new Dictionary<string, FileItem>(files ?? []);
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

    public FileItem? GetFile(string name)
    {
        return _files.TryGetValue(name, out var file) ? file : null;
    }

    public void AddOrUpdateFile(string name, FileItem file)
    {
        if (_files.ContainsKey(name))
        {
            _files[name] = file;
        }
        else
        {
            _files.Add(file.Name, file);
        }
    }

    public bool RemoveFile(string name)
    {
        return _files?.Remove(name) ?? false;
    }

    public bool ContainsFile(string name)
    {
        return _files?.ContainsKey(name) ?? false;
    }

    public IEnumerable<FileItem> GetAllFIles()
    {
        return _files?.Values ?? Enumerable.Empty<FileItem>();
    }


    public void Empty()
    {
        _files = [];
    }


    public void Dispose()
    {
        if (_files != null)
        {
            foreach (var file in _files.Values)
            {
                file?.Dispose();
            }
            _files = [];
        }
    }
}