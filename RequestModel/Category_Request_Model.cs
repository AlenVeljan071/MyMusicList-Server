namespace MyMusicList_Server.RequestModel
{
    public class Category_Request_Model
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
