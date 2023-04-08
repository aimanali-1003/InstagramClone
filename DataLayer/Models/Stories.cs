using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Stories
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string storyImage { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime createdAt { get; set; } = DateTime.Now;
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User user { get; set; }
    }
}
