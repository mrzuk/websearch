using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Email
{
    public class ApproveRejectModel
    {
        public string Comment { get; set; }
        public string ProjectName { get; set; }
        public string ActionBy { get; set; }
    }
}