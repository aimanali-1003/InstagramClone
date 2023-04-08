using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class PostLikes
    {
        [Key]
        [Column(Order=0)]
        public int userId { get; set; }
        public virtual User user { get; set; }
        [Key]
        [Column(Order =1)]
        public int postId { get; set; }
        public virtual Post post { get; set; }
    }
}
