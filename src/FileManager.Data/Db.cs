using Microsoft.EntityFrameworkCore;

namespace FileManager.Data;

using FileManager.Models;

public class Db
{
    public class FileManagerDbContext : DbContext
    {
        public DbSet<FileItem> FileItems { get; set; }
        public DbSet<DirectoryItem> DirectoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DirectoryItem>()
                .HasMany(d => d.SubDirectories)
                .WithOne(f => f.ParentDirectory);
        }
    }
}
