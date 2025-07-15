namespace FileManager.Tests;

using FileManager.Models;

using Newtonsoft.Json.Bson;

using System.IO;
using System.Text;

using Xunit;

public class DirectoryItemTests : IDisposable
{
    private readonly DirectoryItem _testFiles = new("test");
    private readonly FileItem _a = new("a.txt", new MemoryStream(Encoding.UTF8.GetBytes("a")));
    private readonly FileItem _b = new("b.txt", new MemoryStream(Encoding.UTF8.GetBytes("b")));
    private readonly FileItem _c = new("c.txt", new MemoryStream(Encoding.UTF8.GetBytes("c")));

    public DirectoryItemTests()
    {
        _testFiles.AddOrUpdateFile(_a);
        _testFiles.AddOrUpdateFile(_b);
        _testFiles.AddOrUpdateFile(_c);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ConstructorSetName()
    {
        var test = new DirectoryItem("test");
        Assert.Equal("test", test.Name);
    }

    [Fact]
    public void Constructor_SetInvalidName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new FileItem("01test"));
        Assert.Throws<ArgumentException>(() => new FileItem("$test"));
        Assert.Throws<ArgumentException>(() => new FileItem("_test"));
    }

    [Fact]
    public void GetFile_FileDoesNotExist_ReturnsNull()
    {
        var test = new DirectoryItem("test");
        Assert.Null(test.GetFile("test_file.txt"));
    }

    [Fact]
    public void GetFIle_FileExists_ReturnsFile()
    {
        Assert.NotNull(_testFiles.GetFile("a.txt"));
    }
}