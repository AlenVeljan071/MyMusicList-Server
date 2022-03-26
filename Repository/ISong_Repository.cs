namespace MyMusicList_Server.Repository
{
    public interface ISong_Repository
    {
        Task<IEnumerable<Song>> GetSongsAsync();
        Task<IEnumerable<Song>> GetSongsByCategoryAsync(string categoryId);
        Task<IEnumerable<Song>> GetSongsFavoriteAsync();
        Task<Song> GetSongAsync(string id);
        Task<Song> PostSongAsync(Song_Request_Model song);
        Task<Song> PutSongAsync(Song_Response_Model song);
        Task<bool> DeleteSongAsync(string id);
    }
}
