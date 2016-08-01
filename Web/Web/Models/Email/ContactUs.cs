using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Email
{
    public class ContactUs
    {
        [Required(ErrorMessage ="This field is required")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Things you want to write")]
        public string MessageContent { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Your email")]
        public string Email { get; set; }
    }
}