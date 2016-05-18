using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class DetailsView
    {
        public ProjectView ProjectModel { get; set; }

        public bool CanEdit { get; set; }
        
        public bool CanExpressInterest { get; set; }
    }
}