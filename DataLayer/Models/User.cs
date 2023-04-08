using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataLayer.Models
{
    public class User 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string username { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6
        )]
        public string password { get; set; }
        public DateTime dob { get; set; } = new DateTime(2001, 03, 09);
        public bool accountPrivacy { get; set; }
        public string profilePic { get; set; }
        [Required]
        public bool active { get; set; } = true;

        public User (string name, string username, string email, string password, string profilePic="", DateTime dob = new DateTime())
        {
            this.name = name;
            this.email = email;
            this.username = username;
            this.password = password;
            this.accountPrivacy = false;
            this.profilePic = profilePic;
            this.dob = dob;
            this.active = false;
        }
        public User() { }
    }
}
