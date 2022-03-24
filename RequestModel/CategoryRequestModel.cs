using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMusicList_Server.RequestModel
{
    public class CategoryRequestModel
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
