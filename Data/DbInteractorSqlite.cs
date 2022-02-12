using Microsoft.EntityFrameworkCore;
using MyMusicList_Server.Models;

namespace MyMusicList_Server.Data
{
    public class DbInteractorSqlite : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlite(@"Data Source = MusicListDb.db;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
