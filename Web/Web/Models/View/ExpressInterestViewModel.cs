using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class ExpressInterestViewModel
    {
        [Required(ErrorMessage ="This field is required")]

        [DisplayName("Your full name")]
        public string User { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Tell us why you are interested")]
        public string WhyInterested { get; set;}

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}