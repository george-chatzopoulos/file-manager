namespace FileManager.Models.Tests;

using FileManager.Models;

using Xunit;

public class DirectoryItemTests
{
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
        Assert.Throws<ArgumentException>(() => new FileItem("_test/"));
        Assert.Throws<ArgumentException>(() => new FileItem("/_test"));
        Assert.Throws<ArgumentException>(() => new FileItem("/_test/"));
    }
}
