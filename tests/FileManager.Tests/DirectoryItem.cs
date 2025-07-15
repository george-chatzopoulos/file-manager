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
    public void GetFile_FileExists_ReturnsFile()
    {
        Assert.NotNull(_testFiles.GetFile("a.txt"));
    }

    [Fact]
    public void AddOrUpdateFile_AddsFile()
    {
        var test = new FileItem("test.txt");
        Assert.Null(_testFiles.GetFile("test.txt"));

        _testFiles.AddOrUpdateFile(test);
        Assert.Equal(test, _testFiles.GetFile("test.txt"));
    }

    [Fact]
    public void AddOrUpdateFile_UpdatesFile()
    {
        var copy = _testFiles.GetFile("a.txt");
        Assert.Equal(copy?.Content, _testFiles.GetFile("a.txt")?.Content);

        var newA  = new FileItem("a.txt", new MemoryStream(Encoding.UTF8.GetBytes("New a")));
        _testFiles.AddOrUpdateFile(newA);
        Assert.NotEqual(copy?.Content, _testFiles.GetFile("a.txt")?.Content);
    }

    [Fact]
    public void RemoveFile_Succeeds_ReturnsTrue()
    {
        bool result = _testFiles.RemoveFile("a.txt");
        Assert.Null(_testFiles.GetFile("a.txt"));
        Assert.True(result);
    }

    [Fact]
    public void RemoveFile_Fails_ReturnsFalse()
    {
        bool result = _testFiles.RemoveFile("non_existent_file.txt");
        Assert.False(result);
    }

    [Fact]
    public void ContainsFile_FileExists_ReturnsTrue()
    {
        bool result = _testFiles.ContainsFile("a.txt");
        Assert.True(result);
    }

    [Fact]
    public void ContainsFile_FileDoesNotExist_ReturnsFalse()
    {
        bool result = _testFiles.ContainsFile("non_existent_file.txt");
        Assert.False(result);
    }

    [Theory]
    [InlineData("a.txt")]
    [InlineData("b.txt")]
    [InlineData("c.txt")]
    public void GetAllFiles_ReturnsAllFiles(string name)
    {
        var files = _testFiles.GetAllFiles();
        Assert.Contains(files, f => f.Name == name);
    }

    [Fact]
    public void Clear_DeletesAllFiles()
    {
        _testFiles.Clear();
        Assert.Equal(_testFiles.GetAllFiles(), []);
    }
}
