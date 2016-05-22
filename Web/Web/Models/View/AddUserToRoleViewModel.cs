using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class AddUserToRoleViewModel
    {
        public List<string> Roles { get; set; }
        public  Dictionary<string,string> Users { get; set; }
    }
}