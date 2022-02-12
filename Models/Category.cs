using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Song> Songs { get; set; }

    }
}
