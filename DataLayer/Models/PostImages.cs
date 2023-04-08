using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PostImages
    {
        [Key]
        public int id { get; set; }
        public string imageUrl { get; set; }
        [Required]
        public int postId { get; set; }
        public virtual Post post { get; set; }
    }
}
