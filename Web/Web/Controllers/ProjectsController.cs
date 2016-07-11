using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Code;
using Web.DAL;
using Web.Models.Email;
using Web.Models.Helpers;
using Web.Models.View;

namespace Web.Controllers
{
    public class ProjectsController : BaseController
    {

        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            List<ProjectView> project = Db.Project.Include(p => p.Cause_Project).Include(p => p.SuitableLevel_Project).Include(p => p.SuitableSubjects_Project).Where(p => !p.IsApproved).ToList().Select(p => p.ProjectToProjectView()).ToList();
            ViewBag.IsAdmin = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
            ViewBag.CanApprove = ViewBag.IsAdmin;
            ViewBag.CanEdit = ViewBag.IsAdmin;
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(Db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");
            return View(project);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = Db.Project.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(Db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");
            ProjectView projectView = project.ProjectToProjectView();

            string UserId = (User.Identity.IsAuthenticated) ? User.Identity.GetUserId() : "";

            DetailsView viewModel = new DetailsView() { ProjectModel = projectView };

            if (User.Identity.IsAuthenticated) {
                if (UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName)) {
                    viewModel.CanExpressInterest = false;
                    viewModel.CanEdit = true;
                }
                else
                    viewModel.CanExpressInterest = true;
            }
            else {
                viewModel.CanEdit = false;
                viewModel.CanExpressInterest = true;
            }

            return View(viewModel);
        }

        public ActionResult Search()
        {
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");

            ViewBag.Level = new SelectList(Db.SuitableLevel, "Id", "Description");

            SearchModel model = new SearchModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchModel searchModel,int? page)
        {
            var projectList = Db.Project.Where(p => p.IsApproved && p.Cause_Project.Any(cp => cp.CauseId == searchModel.CauseId) && p.SuitableSubjects_Project.Any(cp => cp.SuitableSubjectId == searchModel.SubjectId) && p.SuitableLevel_Project.Any(cp=>cp.SuitableLevelId == searchModel.LevelId)).ToList().Select(p => new SearchResultsModel { Id = p.Id, Title = p.Title, AddedBy=p.AddedByName,Date=p.Date,Cause=p.Cause_Project.Select(c=>c.Cause.Description).ToList(),Subjects=p.SuitableSubjects_Project.Select(c=>c.SuitableSubject.Description).ToList(),Level=p.SuitableLevel_Project.Select(c=>c.SuitableLevel.Description).ToList() });
            int pageN = (page ?? 1);
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");
            ViewBag.Level = new SelectList(Db.SuitableLevel, "Id", "Description");

            MainSearchResults model = new MainSearchResults();
            model.SearchModel = new SearchModel();
            model.SearchResults = projectList.ToPagedList(pageN, 10);

            if(model.SearchResults.Count == 0)
            {

                TempData["success"] = "Search returned no resutls";
            }

            return View("SearchResults",model);
        }

        public ActionResult TakeOnAProject()
        {
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");

            SearchModel model = new SearchModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult TakeOnAProject(SearchModel searchModel, int? page)
        {

            var projectList = Db.Project.Where(p => p.IsApproved && p.Cause_Project.Any(cp => cp.CauseId == searchModel.CauseId) && p.SuitableSubjects_Project.Any(cp => cp.SuitableSubjectId == searchModel.SubjectId) ).ToList().Select(p => new SearchResultsModel { Id = p.Id, Title = p.Title, AddedBy = p.AddedByName, Date = p.Date, Cause = p.Cause_Project.Select(c => c.Cause.Description).ToList(), Subjects = p.SuitableSubjects_Project.Select(c => c.SuitableSubject.Description).ToList(), Level = p.SuitableLevel_Project.Select(c => c.SuitableLevel.Description).ToList() });
            int pageN = (page ?? 1);
            ViewBag.CauseId = searchModel.CauseId;
            ViewBag.SubjectId = searchModel.SubjectId;
            
            if (projectList.Count() > 0)
                return PartialView("~/Views/Projects/Partials/_SearchTakeOnResult.cshtml", projectList.ToPagedList(pageN,10));
            else
            {

                return new JsonResult() { Data = new { info = "Search returned no results" } };
            }

        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ProjectView viewModel = new ProjectView();
            if (User.Identity.IsAuthenticated)
            {
                viewModel.UserName = User.Identity.Name;
                string userId = User.Identity.GetUserId();
                viewModel.UserEmail = Db.AspNetUsers.Where(u => u.Id == userId).Select(c => c.UserName).Single();

            }
            //ViewBag.UserId = new SelectList(Db.AspNetUsers, "Id", "Email");
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(Db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");
            //ViewBag.IsAdmin = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectView project)
        {
            if (ModelState.IsValid)
            {
                project.Date = DateTime.UtcNow;
                if (User.Identity.IsAuthenticated)
                    if (UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName))
                        project.IsApproved = true;

                var projectDb = project.ViewToProject();

                Db.Project.Add(projectDb);
                Db.SaveChanges();
                TempData["success"] = "Project " + project.Title + " created";
                return RedirectToAction("Create");
            }
            else
                TempData["error"] = "Error while saving report";

            return View(project);
        }

        public ActionResult SubmitAProject()
        {

            SubmitAProjectView viewModel = new SubmitAProjectView();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAProject(SubmitAProjectView model)
        {
            try
            {
                Configuration configurations = Configuration.Create(Db);
                EmailMssg mssg = new EmailMssg();
                mssg.IsHtml = true;
                mssg.Receivers = new List<string>() { configurations.ContactFormEmail };
                mssg.SenderAddress = "no-reply@researchforgood.pl";
                mssg.Subject = "Contact request from " + model.FullName;
                mssg.TemplateModel = model;
                mssg.TemplateString = System.IO.File.ReadAllText(Server.MapPath("~/Templates/SubmitAProject.html"));

                EmailSender.Send(configurations, mssg);
                TempData["success"] = "Your project was successfully submitted";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while sending email: " + ex.ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult ApproveProject(ApproveViewModel model)
        {
            Project project = Db.Project.Find(model.ProjectId);

            project.IsApproved = true;
            try
            {
                project.Comment = model.Comment;
                Db.SaveChanges();
                TempData["success"] = "Project " + project.Title + " was approved";
            }
            catch (Exception ex)
            {

                TempData["error"] = "Error while approving report: " + ex.InnerException.ToString();
            }

            try
            {
                string approvedBy = User.Identity.Name;
                string receiver = project.AddedByEmail;
                Configuration configurations = Configuration.Create(Db);
                EmailMssg mssg = new EmailMssg();
                mssg.IsHtml = true;
                mssg.Receivers = new List<string>() { receiver };
                mssg.SenderAddress = "no-reply@researchforgood.pl";
                mssg.Subject = "Your project " +project.Title + " has been approved";
                mssg.TemplateModel = new ApproveRejectModel() { Comment = model.Comment, ActionBy = approvedBy, ProjectName = project.Title };
                mssg.TemplateString = System.IO.File.ReadAllText(Server.MapPath("~/Templates/ProjectApproved.html"));

                EmailSender.Send(configurations, mssg);
            }
            catch (Exception ex) {
                TempData["error"] = "Error while sending approvment email" + ex.ToString();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RejectProject(ApproveViewModel model)
        {
            Project project = Db.Project.Find(model.ProjectId);

            project.WasRevised = true;
            project.IsApproved = false;
            try
            {
                project.Comment = model.Comment;
                Db.SaveChanges();
                TempData["success"] = "Project " + project.Title + " was approved";
            }
            catch (Exception ex)
            {

                TempData["error"] = "Error while approving report: " + ex.InnerException.ToString();
            }

            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Project.Include(p => p.SuitableLevel_Project).Include(p=>p.SuitableSubjects_Project).Include(p=>p.Cause_Project).Find(id);

            Project project = Db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cause = new SelectList(Db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(Db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(Db.SuitableSubject, "Id", "Description");

            ProjectView viewModel = project.ProjectToProjectView();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExpressInterest(ExpressInterestViewModel viewModel)
        {
            try
            {
                
                    InterestedUsers_Projects user2Project = new InterestedUsers_Projects() { ProjectId = viewModel.ProjectId, UserEmail = viewModel.Email,UserName = viewModel.User};
                    Db.InterestedUsers_Projects.Add(user2Project);

                    int written = Db.SaveChanges();

                    try
                    {
                        Configuration configurations = Configuration.Create(Db);
                        EmailMssg mssg = new EmailMssg();
                        mssg.IsHtml = true;
                        mssg.Receivers = new List<string>() { configurations.ContactFormEmail };
                        mssg.SenderAddress = configurations.ftpConfig.Login;
                        mssg.Subject = "User " + viewModel.User + " expressed interest in project ";
                        mssg.TemplateModel = new ExpressInterestEmailModel() { ProjectName = viewModel.ProjectName,UserComment = viewModel.WhyInterested, UserEmail = viewModel.Email, UserName = viewModel.User };
                        mssg.TemplateString = System.IO.File.ReadAllText(Server.MapPath("~/Templates/InterestedInProject.html"));

                        EmailSender.Send(configurations, mssg);
                    }
                    catch (Exception ex)
                    {
                        string error = "Error while sending approvment email" + ex.ToString();
                        TempData["error"] = error;
                        return RedirectToAction("Details", new { id = viewModel.ProjectId });
                    }
                        return new JsonResult() { Data = new { success = true } };
        
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while expressing interest" + ex.ToString();
                return RedirectToAction("Details", new { id = viewModel.ProjectId });
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectView project)
        {
            try {
                var projectDB = Db.Project.Find(project.Id);
                if (projectDB != null)
                {
                    if (ModelState.IsValid)
                    {

                        var projectModel = project.ViewToProject();

                        Db.Set<Project>().AddOrUpdate(projectModel);

                        //remove deselected cause
                        projectDB.Cause_Project.Where(cp => cp.ProjectId == project.Id && !project.Causes.Contains(cp.CauseId)).ToList().ForEach(cp => Db.Entry(cp).State = EntityState.Deleted);
                        //add selected cause
                        List<int> causesExisting = projectDB.Cause_Project.Select(c => c.CauseId).ToList();
                        List<Cause_Project> causesToAdd = project.Causes.Where(c => !causesExisting.Contains(c)).ToList().Select(c => new Cause_Project() { CauseId = c, ProjectId = project.Id }).ToList();
                        causesToAdd.ForEach(c => projectDB.Cause_Project.Add(c));

                        //remove deselected levels
                        projectDB.SuitableLevel_Project.Where(cp => cp.ProjectId == project.Id && !project.SuitableLevels.Contains(cp.SuitableLevelId)).ToList().ForEach(cp => Db.Entry(cp).State = EntityState.Deleted);
                        //add selected levels
                        List<int> levelsExisting = projectDB.SuitableLevel_Project.Select(c => c.SuitableLevelId).ToList();
                        List<SuitableLevel_Project> levelToAdd = project.Causes.Where(c => !levelsExisting.Contains(c)).ToList().Select(c => new SuitableLevel_Project() { SuitableLevelId = c, ProjectId = project.Id }).ToList();
                        levelToAdd.ForEach(c => projectDB.SuitableLevel_Project.Add(c));

                        // remove deselected Subjects
                        projectDB.SuitableSubjects_Project.Where(cp => cp.ProjectId == project.Id && !project.SuitableSubjects.Contains(cp.SuitableSubjectId)).ToList().ForEach(cp => Db.Entry(cp).State = EntityState.Deleted);
                        //add selected Subjects
                        List<int> subjectExisting = projectDB.SuitableSubjects_Project.Select(c => c.SuitableSubjectId).ToList();
                        List<SuitableSubjects_Project> subjectToAdd = project.SuitableSubjects.Where(c => !subjectExisting.Contains(c)).ToList().Select(c => new SuitableSubjects_Project() { SuitableSubjectId = c, ProjectId = project.Id }).ToList();
                        subjectToAdd.ForEach(c => projectDB.SuitableSubjects_Project.Add(c));

                        Db.SaveChanges();
                        TempData["success"] = "Project " + projectModel.Title + " successfuly edited";
                        return new JsonResult() { Data = new { success = true } };
                    }
                }
            }
            catch (Exception ex) {
                TempData["error"] = "Error while editing report " + ex.InnerException.ToString();
            }

            
            return new JsonResult() { Data = new { success = false } };
        }

        // GET: Projects/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = Db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            TempData["success"] = "Report " + project.Title + " removed";
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = Db.Project.Find(id);

            if (project != null)
            {
                Db.Project.Remove(project);
                try
                {
                    Db.SaveChanges();
                    TempData["success"] = "Report " + project.Title + " successfully deleted";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error while deleting report: " + ex.InnerException.ToString();
                }
            }
            else
                TempData["error"] = "Report was already deleted";

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
