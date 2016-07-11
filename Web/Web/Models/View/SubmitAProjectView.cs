using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class SubmitAProjectView
    {
        private const string RequiredErrorMessage = "This field is required";

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Organization")]
        public string Organization { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("project area")]
        public string ProjectArea { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("project description (min. 200 characters)")]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("why do you thing it is high impact")]
        public string HighImpact { get; set; }


    }
}