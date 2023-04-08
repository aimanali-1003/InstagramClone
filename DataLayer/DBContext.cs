using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram
{
    public class DBContext:DbContext
    {
        public DBContext() : base()
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostImages> PostImages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<PostLikes> PostLikes { get; set; }
        public virtual DbSet<Followers> Followers { get; set; }
        public virtual DbSet<Stories> Stories { get; set; }
    }
}
