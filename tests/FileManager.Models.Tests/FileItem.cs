namespace FileManager.Models.Tests;

using FileManager.Models;

using Xunit;

public class FileItemTests
{
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
    public void Size_WithNullContent_ReturnsZero()
    {
        var file = new FileItem("test.txt");
        Assert.Equal(0, file.Size);
    }

    [Fact]
    public void Constructor_SetPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new FileItem("test.txt", "home/"));
        Assert.Throws<ArgumentException>(() => new FileItem("test.txt", "/home"));
    }

    [Theory]
    [InlineData("/", "/test.txt")]
    [InlineData("/home/", "/home/test.txt")]
    [InlineData("/home/desktop123/", "/home/desktop123/test.txt")]
    public void Path_ParsesCorrectly(string path, string expectedRes)
    {
        var file = new FileItem("test.txt", path);
        Assert.Equal(expectedRes, file.Path);
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
