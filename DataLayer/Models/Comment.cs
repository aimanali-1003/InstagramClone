using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string description { get; set; }
        public DateTime time { get; set; }
        [Required]
        public int postId { get; set; }
        public virtual Post post { get; set; }

        [Required]
        public int userId { get; set; }
        public virtual User user { get; set; }
    }
}
