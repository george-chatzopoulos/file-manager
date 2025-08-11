using Microsoft.EntityFrameworkCore;

namespace FileManager.Data;

public class Db
{
    public class FileManagerDbContext : DbContext
    {
        public string DbPath { get; }

        public FileManagerDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "file-manager.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
