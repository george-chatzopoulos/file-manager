namespace FileManager.Tests;

using FileManager.Models;
using System.IO;
using System.Text;
using Xunit;

public class FileItemTests : IDisposable
{
  private readonly MemoryStream _testStream;

  public FileItemTests()
  {
    _testStream = new MemoryStream(Encoding.UTF8.GetBytes("Hello, world!"));
  }

  public void Dispose()
  {
    GC.SuppressFinalize(this);
  }

  [Fact]
  public void Constructor_SetNameAndExtension()
  {
    var file = new FileItem("test.txt");

    Assert.Equal("test.txt", file.Name);
    Assert.Equal(".txt", file.Extension);
  }

  [Fact]
  public void Constructor_SetInvalidName_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => new FileItem("invalid name")); // Missing extension
    Assert.Throws<ArgumentException>(() => new FileItem("invalid:name.txt")); // Invalid characters
    Assert.Throws<ArgumentException>(() => new FileItem("")); // Empty name
  }

  [Fact]
  public void SetName_ValidName_UpdatesNameAndExtension()
  {
    var file = new FileItem("test.txt");
    file.SetName("newfile.docx");

    Assert.Equal("newfile.docx", file.Name);
    Assert.Equal(".docx", file.Extension);
  }

  [Fact]
  public void Constructor_SetContentAndSize()
  {
    var file = new FileItem("test.txt", _testStream);

    Assert.True(file.Size > 0);
    Assert.True(file.Content != null);
  }

  [Fact]
  public void Size_ReturnsStreamLength()
  {
    var file = new FileItem("test.txt", _testStream);
    Assert.Equal("Hello, world!".Length, file.Size);
  }

  [Fact]
  public void Size_WithNullContent_ReturnsZero()
  {
    var file = new FileItem("test.txt");
    Assert.Equal(0, file.Size);
  }

  [Fact]
  public void Dispose_ClosesUnderlyingStream()
  {
    var file = new FileItem("test.txt", _testStream );

    file.Dispose();

    Assert.Throws<ObjectDisposedException>(() => _testStream.ReadByte());
  }

  [Theory]
  [InlineData("document.pdf", ".pdf")]
  [InlineData("archive.tar.gz", ".gz")]
  public void Extension_ParsesCorrectly(string name, string expectedExt)
  {
    var file = new FileItem(name);
    Assert.Equal(expectedExt, file.Extension);
  }
}
