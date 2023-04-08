using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public DateTime time { get; set; }
        public bool active { get; set; } = true;

        [Required]
        [ForeignKey("userId")]
        public virtual User user { get; set; }
        public int userId { get; set; }

        //public int 
    }
}
