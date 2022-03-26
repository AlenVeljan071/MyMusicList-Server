namespace MyMusicList_Server.Repository
{
    public class Song_Repository : ISong_Repository
    {
        private readonly DbInteractorSqlite _context;
        public Song_Repository(DbInteractorSqlite context)
        {
            _context = context;
        }
        public async Task<bool> DeleteSong(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<Song> GetSong(string id)
        {
            var song = await _context.Songs.Where(x => x.SongId == id).FirstOrDefaultAsync();
            return song;
        }

        public async Task<IEnumerable<Song>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByCategory(string categoryId)
        {
            return await _context.Songs.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsFavorite()
        {
            return await _context.Songs.Where(x => x.IsAFavorite == true).ToListAsync();
        }
        public async Task<Song> PostSong(Song_Request_Model song)
        {
            var dbSong = new Song
            {
                SongId = Guid.NewGuid().ToString(),
                SongName = song.SongName,
                SongRating = song.SongRating,
                Artist = song.Artist,
                IsAFavorite = song.IsAFavorite,
                CategoryId = song.CategoryId,
                CreatedDate = DateTime.UtcNow,
                Url = song.Url,
            };
            _context.Songs.Add(dbSong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return dbSong;
        }

        public async Task<Song> PutSong(Song_Response_Model song)
        {
            var songF = _context.Songs.Where(x => x.SongId == song.SongId).FirstOrDefault();
            if (songF == null) return null;
            songF.SongName = song.SongName;
            songF.SongRating = song.SongRating;
            songF.Artist = song.Artist;
            songF.Url = song.Url;
            songF.IsAFavorite = song.IsAFavorite;
            songF.CategoryId = song.CategoryId;
            songF.UpdatedDate = DateTime.UtcNow;
            songF.CreatedDate = songF.CreatedDate;
            _context.Update(songF);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return songF;
        }
    }
}
