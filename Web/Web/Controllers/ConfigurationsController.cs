using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.DAL;
using Web.Models.View;
using Web.Helpers;
using Web.Code;
using Web.Models.Email;

namespace Web.Controllers
{
    [Authorize]
    public class ConfigurationsController : BaseController
    {
        WebDbEntities db = new WebDbEntities();
        // GET: Configuration
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ConfigurationView modelView = new ConfigurationView();

            var configs = db.Configurations.Select(c => c);
            modelView.contactUsEmail = configs.Where(c => c.Key == "contactUsEmail").Select(c => c.Value).FirstOrDefault();
            modelView.emailLogin = configs.Where(c => c.Key == "emailLogin").Select(c => c.Value).FirstOrDefault();
            modelView.emailPassword = configs.Where(c => c.Key == "emailPassword").Select(c => c.Value).FirstOrDefault();
            modelView.emailServer = configs.Where(c => c.Key == "emailServer").Select(c => c.Value).FirstOrDefault();
            modelView.smttpPort = Int32.Parse(configs.Where(c => c.Key == "smttpPort").Select(c => c.Value).FirstOrDefault());

            return View(modelView);
        }

        // GET: Configuration
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ConfigurationView modelView)
        {
            List<PropertyValue> configs = modelView.GetConfigurationValues();

            foreach(PropertyValue val in configs)
            {
                Configurations config = new Configurations() { Key = val.Name, Value = val.Value };
                var configDB = db.Configurations.Where(c => c.Key == config.Key).Select(c => c).First();
                configDB.Value = config.Value;
            }


            db.SaveChanges();

            Configuration configurations = Configuration.Create(db);
            EmailMssg mssg = new EmailMssg();
            mssg.IsHtml = true;
            mssg.Receivers = new List<string>() { "mrzuk@op.pl" };
            mssg.SenderAddress = "no-reply@costam.pl";
            mssg.Subject = "test";
            mssg.TemplateModel = new ContactUs() { From = "Ktos tam", MessageContent = "hahaha" };
            mssg.TemplateString = System.IO.File.ReadAllText(Server.MapPath("~/Templates/ContactUsTemplate.html"));

            EmailSender.Send(configurations, mssg);

            return View();
        }
    }
}