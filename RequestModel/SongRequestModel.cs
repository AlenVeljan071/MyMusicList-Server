
using System;
using System.ComponentModel.DataAnnotations;

namespace MyMusicList_Server.RequestModel
{
    public class SongRequestModel
    {
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
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public string CategoryId { get; set; }

    }
}
