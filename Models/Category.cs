namespace MyMusicList_Server.Models
{
    public class Category
    {
        [Key]
        [StringLength(36)]
        public string CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        [Url]
        [Required]
        public string ImageUrl { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }

        public ICollection<Song> Songs { get; set; }

    }
}
