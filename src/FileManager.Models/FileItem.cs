namespace FileManager.Models;

using System.Text.RegularExpressions;


public sealed class FileItem : IDisposable
{
  public string Name { get; set; }
  public string Extension { get; private set; }
  public Stream Content { get; set; }
  public long Size => Content?.Length ?? 0;

  public FileItem(string name, Stream? content = null)
  {
    SetName(name);
    Content = content ?? Stream.Null;
  }

  public void SetName(string name)
  {
    if (!IsNameValid(name))
      throw new ArgumentException("Invalid file name", nameof(name));

    Name = name;
    Extension = Path.GetExtension(name);
  }


  private bool IsNameValid(string name)
  {
    bool isNameNotEmpty = !string.IsNullOrWhiteSpace(name);
    string pattern = @"^[\w\s-]+(\.[\w\.]+[\w]+)$";
    bool hasNameValidPattern = Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);

    return isNameNotEmpty && hasNameValidPattern;
  }

  public void Dispose()
  {
    Content?.Dispose();
  }
}
