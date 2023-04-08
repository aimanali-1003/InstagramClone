using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class ProfileData
    {
        public User user { get; set; }
        public List<PostWithImages> postWithImages { get; set; }
        public bool following = false;
    }
}
