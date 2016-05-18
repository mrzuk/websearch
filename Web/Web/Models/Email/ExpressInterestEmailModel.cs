using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Email
{
    public class ExpressInterestEmailModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserComment { get; set; }
        public string ProjectName { get; set; }
    }
}