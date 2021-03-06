namespace MyMusicList_Server.Data
{
    public class DbInteractorSqlite : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlite(@"Data Source = MyMusicDb.db;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
