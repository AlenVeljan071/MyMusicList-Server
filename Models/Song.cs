namespace MyMusicList_Server.Models
{
    public class Song
    {
        [Key]
        [StringLength(36)]
        public string SongId { get; set; }
        [Required]
        [StringLength(50)]
        public string SongName { get; set; }
        [Required]
        [StringLength(50)]
        public string Artist { get; set; }
        [Url]
        [Required]
        public string Url { get; set; }
        [Required]
        [Range(1, 5)]
        public int SongRating { get; set; }
        public bool IsAFavorite { get; set; }
        [Required]
        public string CategoryId { get; set; }
        #region Data Trail
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        #endregion
    }
}
