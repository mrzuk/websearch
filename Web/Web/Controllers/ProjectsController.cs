using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.DAL;
using Web.Models.Helpers;
using Web.Models.View;

namespace Web.Controllers
{
    public class ProjectsController : BaseController
    {
        private WebDbEntities db = new WebDbEntities();

        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            List<ProjectView> project = db.Project.Include(p => p.AspNetUsers).Include(p => p.Cause_Project).Include(p => p.SuitableLevel_Project).Include(p => p.SuitableSubjects_Project).Where(p => !p.IsApproved).ToList().Select(p => p.ProjectToProjectView()).ToList();
            ViewBag.IsAdmin = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
            ViewBag.CanApprove = ViewBag.IsAdmin;
            ViewBag.CanEdit = ViewBag.IsAdmin;
            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description");
            return View(project);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);

            if (User.Identity.IsAuthenticated)
                ViewBag.CanEdit = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
            else
                ViewBag.CanEdit = false;

            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description");
            ProjectView projectView = project.ProjectToProjectView();

            string UserId = (User.Identity.IsAuthenticated) ? User.Identity.GetUserId() : "";
            bool isUserAlreadyInterested = false;
            if (!string.IsNullOrEmpty(UserId))
            {
                var intereted = db.InterestedUsers_Projects.Where(iup => iup.ProjectId == id && iup.UserId == UserId).FirstOrDefault();
                if (intereted != null)
                    isUserAlreadyInterested = true;
            }

            DetailsView viewModel = new DetailsView() { IsUserInterested = isUserAlreadyInterested, ProjectModel = projectView };

            return View(viewModel);
        }

        public ActionResult Search()
        {
            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description");

            SearchModel model = new SearchModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchModel model)
        {
            var projectList = db.Project.Where(p => p.IsApproved && p.Cause_Project.Any(cp => cp.CauseId == model.CauseId) && p.SuitableSubjects_Project.Any(cp => cp.SuitableSubjectId == model.SubjectId)).ToList().Select(p => new SearchResultsModel { Id = p.Id, Title = p.Title });

            if (projectList.Count() > 0)
                return PartialView("~/Views/Projects/Partials/_SearchResults.cshtml", projectList);
            else
            {

                return new JsonResult() { Data = new { info = "Search returned no results" } };
            }
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description");
            ViewBag.IsAdmin = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectView project)
        {
            if (ModelState.IsValid)
            {
                project.UserId = User.Identity.GetUserId();
                project.Date = DateTime.UtcNow;
                var projectDb = project.ViewToProject();

                db.Project.Add(projectDb);
                db.SaveChanges();
                TempData["success"] = "Project " + project.Title + " created";
                return RedirectToAction("Create");
            }
            else
                TempData["error"] = "Error while saving report";

            return View(project);
        }

        [HttpPost]
        public ActionResult ApproveProject(int id, string comment)
        {
            Project project = db.Project.Find(id);

            project.IsApproved = true;
            try
            {
                db.SaveChanges();
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

            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description");

            ProjectView viewModel = project.ProjectToProjectView();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExpressInterest(int projectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                InterestedUsers_Projects user2Project = new InterestedUsers_Projects() { ProjectId = projectId, UserId = User.Identity.GetUserId() };
                db.InterestedUsers_Projects.Add(user2Project);
                db.SaveChanges();
                return new JsonResult() { Data = new { success = true } };
            }
            else
                return new JsonResult() { Data = new { userNotLogged = true } };
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectView project)
        {
            try {
                var projectDB = db.Project.Find(project.Id);
                if (projectDB != null)
                {
                    if (ModelState.IsValid)
                    {

                        var projectModel = project.ViewToProject();

                        db.Set<Project>().AddOrUpdate(projectModel);

                        //remove deselected cause
                        projectDB.Cause_Project.Where(cp => cp.ProjectId == project.Id && !project.Causes.Contains(cp.CauseId)).ToList().ForEach(cp => db.Entry(cp).State = EntityState.Deleted);
                        //add selected cause
                        List<int> causesExisting = projectDB.Cause_Project.Select(c => c.CauseId).ToList();
                        List<Cause_Project> causesToAdd = project.Causes.Where(c => !causesExisting.Contains(c)).ToList().Select(c => new Cause_Project() { CauseId = c, ProjectId = project.Id }).ToList();
                        causesToAdd.ForEach(c => projectDB.Cause_Project.Add(c));

                        //remove deselected levels
                        projectDB.SuitableLevel_Project.Where(cp => cp.ProjectId == project.Id && !project.SuitableLevels.Contains(cp.SuitableLevelId)).ToList().ForEach(cp => db.Entry(cp).State = EntityState.Deleted);
                        //add selected levels
                        List<int> levelsExisting = projectDB.SuitableLevel_Project.Select(c => c.SuitableLevelId).ToList();
                        List<SuitableLevel_Project> levelToAdd = project.Causes.Where(c => !levelsExisting.Contains(c)).ToList().Select(c => new SuitableLevel_Project() { SuitableLevelId = c, ProjectId = project.Id }).ToList();
                        levelToAdd.ForEach(c => projectDB.SuitableLevel_Project.Add(c));

                        // remove deselected Subjects
                        projectDB.SuitableSubjects_Project.Where(cp => cp.ProjectId == project.Id && !project.SuitableSubjects.Contains(cp.SuitableSubjectId)).ToList().ForEach(cp => db.Entry(cp).State = EntityState.Deleted);
                        //add selected Subjects
                        List<int> subjectExisting = projectDB.SuitableSubjects_Project.Select(c => c.SuitableSubjectId).ToList();
                        List<SuitableSubjects_Project> subjectToAdd = project.SuitableSubjects.Where(c => !subjectExisting.Contains(c)).ToList().Select(c => new SuitableSubjects_Project() { SuitableSubjectId = c, ProjectId = project.Id }).ToList();
                        subjectToAdd.ForEach(c => projectDB.SuitableSubjects_Project.Add(c));

                        db.SaveChanges();
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
            Project project = db.Project.Find(id);
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
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
