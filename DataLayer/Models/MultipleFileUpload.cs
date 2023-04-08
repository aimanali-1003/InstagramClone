using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class MultipleFileUpload
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public List<HttpPostedFileBase> files { get; set; }
    }
}
