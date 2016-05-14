using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        private WebDbEntities1 db = new WebDbEntities1();

        // GET: Projects
        public ActionResult Index()
        {
            List<ProjectView> project = db.Project.Include(p => p.AspNetUsers).Include(p => p.Cause_Project).Include(p => p.SuitableLevel_Project).Include(p => p.SuitableSubjects_Project).Where(p=> !p.IsApproved).ToList().Select(p=> p.ProjectToProjectView()).ToList();
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
            if (project == null)
            {
                return HttpNotFound();
            }
            ProjectView projectView = project.ProjectToProjectView();
            return View(projectView);
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
            var projectList = db.Project.Where(p => p.Cause_Project.Any(cp => cp.CauseId == model.CauseId) && p.SuitableSubjects_Project.Any(cp=> cp.SuitableSubjectId == model.SubjectId)).ToList().Select(p => new SearchResultsModel { Id = p.Id, Title = p.Title });

            if (projectList.Count() > 0)
                return PartialView("~/Views/Projects/Partials/_SearchResults.cshtml", projectList);
            else {

                return new JsonResult() { Data = new { info = "Search returned no results" } };
            }
        }

        // GET: Projects/Create
        public ActionResult Create()
        {

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Cause = new SelectList(db.Cause, "Id", "Description");
            ViewBag.SuitableLevel = new SelectList(db.SuitableLevel, "Id", "Description");
            ViewBag.SuitableSubject = new SelectList(db.SuitableSubject, "Id", "Description"); 
            return View();
        }

       
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
                return RedirectToAction("Index");
            }

           
            return View(project);
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

            ProjectView viewModel= project.ProjectToProjectView();
            return View(viewModel);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectView project)
        {
            if (ModelState.IsValid)
            {
                var projectModel = project.ViewToProject();
                db.Entry(projectModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(project);
        }

        // GET: Projects/Delete/5
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
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
