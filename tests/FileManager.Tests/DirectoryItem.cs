namespace FileManager.Tests;

using FileManager.Models;

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
}