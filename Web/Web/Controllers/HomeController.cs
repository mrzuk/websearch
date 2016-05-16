using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Web.Models.Email;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Contact()
        {

            ContactUs viewModel = new ContactUs();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactUs model)
        {
            try { 
            Configuration configurations = Configuration.Create(Db);
            EmailMssg mssg = new EmailMssg();
            mssg.IsHtml = true;
            mssg.Receivers = new List<string>() { configurations.ContactFormEmail };
            mssg.SenderAddress = "no-reply@researchforgood.pl";
            mssg.Subject = "Contact request from " + model.FullName;
            mssg.TemplateModel = model;
            mssg.TemplateString = System.IO.File.ReadAllText(Server.MapPath("~/Templates/ContactUsTemplate.html"));

            EmailSender.Send(configurations, mssg);
            TempData["success"] = "Your email was successfully sent";
            }
            catch(Exception ex)
            {
                TempData["error"] = "Error while sending email: " + ex.InnerException.ToString();
            }
            return View();
        }
    }
}