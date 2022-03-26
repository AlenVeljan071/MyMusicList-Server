namespace MyMusicList_Server.Repository
{
    public interface ISong_Repository
    {
        Task<IEnumerable<Song>> GetSongs();
        Task<IEnumerable<Song>> GetSongsByCategory(string categoryId);
        Task<IEnumerable<Song>> GetSongsFavorite();
        Task<Song> GetSong(string id);
        Task<Song> PostSong(Song_Request_Model song);
        Task<Song> PutSong(Song_Response_Model song);
        Task<bool> DeleteSong(string id);
    }
}
