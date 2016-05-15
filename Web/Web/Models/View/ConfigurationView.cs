using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class ConfigurationView
    {
        [Required(ErrorMessage="This field is required")]
        public string contactUsEmail { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string emailServer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string emailLogin { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string emailPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int smttpPort { get; set; }

    }
}