using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Followers
    {
        [Key]
        [Column(Order=0)]
        public int FollowedBy { get; set; }
        [Key]
        [Column(Order = 1)]
        public int FollowedUser { get; set; }

        public virtual User user { get; set; }

    }
}
