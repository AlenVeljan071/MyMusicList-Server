using System.ComponentModel.DataAnnotations;

namespace MyMusicList_Server.RequestModel
{
    public class CategoryRequestModel
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}
