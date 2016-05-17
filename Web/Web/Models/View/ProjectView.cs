using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class ProjectView
    {
        private const string RequiredErrorMessage = "This field is required";
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Title { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Area")]
        public string ProjectArea { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Description { get; set; }

        [DisplayName("Specific Projects")]
        public string SpecificProjects { get; set; }


        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Impact { get; set; }


        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Skills { get; set; }

        [DisplayName("Source link")]
        public string SourceLink { get; set; }

        [DisplayName("Suggested reading")]
        public string SuggestedReading { get; set; }

        [DisplayName("Suggested methods")]
        public string SuggestedMethods { get; set; }
        public string UserId { get; set; }

        public System.DateTime? Date { get; set; }

        [DisplayName("Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("Added by")]
        public string UserName { get; set; }

        public List<Cause> CauseCollection { get; set; }

        public List<SuitableLevel> LevelCollection { get; set; }

        public List<SuitableSubject> SubjectCollection { get; set; }


        [Required(ErrorMessage = RequiredErrorMessage)]

        public List<int> Causes{get;set;}

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Levels")]
        public List<int> SuitableLevels { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Subjects")]
        public List<int> SuitableSubjects { get; set; }

        [DisplayName("Interested users")]
        public List<string> InterestedUsers { get; set; }


     }

    public class Cause
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class SuitableLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }


    public class SuitableSubject
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}