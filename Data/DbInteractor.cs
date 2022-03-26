namespace MyMusicList_Server.Data
{
    public class DbInteractor : DbContext
    {
        public DbInteractor(DbContextOptions<DbInteractor> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
