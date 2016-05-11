using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.View;

namespace Web.Controllers
{
    public class LayoutController : BaseController
    {
       
        [ChildActionOnly]
        [ActionName("IsAdmin")]
        public ActionResult IsAdmin()
        {
            IsUserAdmin isAdmin = new IsUserAdmin();

            if (User.Identity.IsAuthenticated)
            {
                if (UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName))
                    isAdmin.IsAdministrator = true;

            }
            return PartialView("_IsAdmin", isAdmin);
        }
    }
}