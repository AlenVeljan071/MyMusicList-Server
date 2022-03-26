namespace MyMusicList_Server.Extensions
{
    public static class Extensions
    {
        public static Song_Response_Model SongResponse(this Song dbSong)
        {
            return new Song_Response_Model()
            {
                SongId = dbSong.SongId,
                SongName = dbSong.SongName,
                Artist = dbSong.Artist,
                IsAFavorite = dbSong.IsAFavorite,
                CategoryId = dbSong.CategoryId,
                CreatedDate = dbSong.CreatedDate,
                Url = dbSong.Url,
                SongRating = dbSong.SongRating,
            };
        }

        public static Category_Response_Model CategoryResponse(this Category category)
        {
            return new Category_Response_Model()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }
    }
}
