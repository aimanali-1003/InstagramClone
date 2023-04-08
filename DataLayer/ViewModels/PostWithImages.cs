using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class PostWithImages
    {
        [Key]
        public int id { get; set; }
        public Post post { get; set; }
        public List<PostLikes> likes { get; set; }
        public List<PostImages> images { get; set; }

        public virtual Post postRef { get; set; }
        public virtual PostImages postImages { get; set; }
    }
}
