namespace FileManager.Models.Tests;

using FileManager.Models;

using Xunit;

public class DirectoryItemTests
{
    [Fact]
    public void ConstructorSetName()
    {
        var test = new DirectoryItem("test/");
        Assert.Equal("test/", test.Name);
    }

    [Fact]
    public void Constructor_SetInvalidName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new FileItem("01test"));
        Assert.Throws<ArgumentException>(() => new FileItem("$test"));
        Assert.Throws<ArgumentException>(() => new FileItem("_test"));
        Assert.Throws<ArgumentException>(() => new FileItem("_test/"));
        Assert.Throws<ArgumentException>(() => new FileItem("/_test"));
        Assert.Throws<ArgumentException>(() => new FileItem("/_test/"));
    }

    [Fact]
    public void Constructor_SetPath_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new DirectoryItem("test/", "incorrect_path/"));
        Assert.Throws<ArgumentException>(() => new DirectoryItem("test/", "/incorrect_path"));
    }

    [Theory]
    [InlineData("/", "/test/")]
    [InlineData("/home/", "/home/test/")]
    [InlineData("/home/desktop123/", "/home/desktop123/test/")]
    public void Path_ParsesCorrectly(string path, string expectedRes)
    {
        var file = new DirectoryItem("test/", path);
        Assert.Equal(expectedRes, file.Path);
    }
}
