using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DAL;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private WebDbEntities db { get; set; }
        public virtual WebDbEntities Db
        {
            get
            {
                return db ?? new WebDbEntities();
            }
            protected set
            {
                db = value;
            }
        }

        protected ApplicationUserManager _userManager;
        public virtual ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }


        public readonly string AdminRoleName = "Admin";

    }
}