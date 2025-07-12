namespace FileManager.Models;

using FileManager.Common;

public sealed class FileItem : IDisposable
{
  public string Name { get; set; } = string.Empty;
  public string Extension { get; set; } = string.Empty;
  public Stream Content { get; set; }
  public long Size => Content?.Length ?? 0;

  public FileItem(string name, Stream? content = null)
  {
    SetName(name);
    Content = content ?? Stream.Null;
  }

  public void SetName(string name)
  {
    if (!Models.IsFileNameValid(name))
      throw new ArgumentException("Invalid file name", nameof(name));

    Name = name;
    Extension = Path.GetExtension(name);
  }


  public void Dispose()
  {
    Content?.Dispose();
  }
}
