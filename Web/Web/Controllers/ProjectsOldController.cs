//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Web.DAL;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Web.Models.View;

//namespace Web.Controllers
//{
//    [Authorize]
//    public class ProjectsOldController : BaseController
//    {
//        private WebDbEntities1 db = new WebDbEntities1();

//        public ActionResult Index(List<Project> projects = null)
//        {
//            if (projects == null)
//            {
//                projects = db.Project.Include(p => p.AspNetUsers).Include(p => p.Cause).Include(p => p.SuitableSubject).Include(p=>p.SuitableLevel).Where(p => !p.IsApproved).ToList();
               
//            }
//            else
//            {
//                if (projects.Count() == 0)
//                    RedirectToAction("Search");
//            }
//            return View(projects);
//        }

//        public ActionResult Search()
//        {
//            ProjectSearch search = new ProjectSearch();
//            return View("",search);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Search(ProjectSearch project)
//        {
//            List<Project> projects = db.Project.Include(c => c.Cause).Where(p => p.Cause.Description.ToLower().Contains(project.Cause.ToLower()) && p.SuitableSubject.Description.ToLower().Contains(project.Subject.ToLower())).Select(p => p).ToList();
//            return PartialView("~/Views/Shared/_ProjectList.cshtml",projects);
//        }

//        // GET: Projects/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Project project = db.Project.Find(id);
//            if (project == null)
//            {
//                return HttpNotFound();
//            }
//            return View(project);
//        }

//        // GET: Projects/Create
//        public ActionResult Create()
//        {
//            ViewBag.IsUserAdmin = UserManager.IsInRole(User.Identity.GetUserId(), this.AdminRoleName);
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
//            ViewBag.CauseId = new SelectList(db.Cause, "Id", "Description");
//            ViewBag.SuitableSubjectId = new SelectList(db.SuitableSubject, "Id", "Description");
//            ViewBag.SuitableLevelId = new SelectList(db.SuitableLevel, "Id", "Description");
//            return View();
//        }

//        // POST: Projects/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,ProjectArea,Description,SpecificProjects,Impact,SuitableSubjectId,CauseId,SuitableLevelId,Skills,SourceLink,SuggestedReading,SuggestedMethods,UserId,Date,IsApproved")] Project project)
//        {
//            string userId= User.Identity.GetUserId();
//            if (ModelState.IsValid)
//            {
//                project.UserId = userId;
//                project.Date = DateTime.UtcNow;
//                db.Project.Add(project);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", project.UserId);
//            ViewBag.CauseId = new SelectList(db.Cause, "Id", "Description", project.CauseId);
//            ViewBag.SuitableSubjectId = new SelectList(db.SuitableSubject, "Id", "Description", project.SuitableSubjectId);
//            return View(project);
//        }
        
//        [Authorize(Roles ="Admin")]
//        // GET: Projects/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Project project = db.Project.Find(id);
//            if (project == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", project.UserId);
//            ViewBag.CauseId = new SelectList(db.Cause, "Id", "Description", project.CauseId);
//            ViewBag.SuitableSubjectId = new SelectList(db.SuitableSubject, "Id", "Description", project.SuitableSubjectId);
//            ViewBag.SuitableLevelId = new SelectList(db.SuitableLevel, "Id", "Description",project.SuitableLevelId);
//            return View(project);
//        }

//        // POST: Projects/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,ProjectArea,Description,SpecificProjects,Impact,SuitableSubjectId,CauseId,SuitableLevel,Skills,SourceLink,SuggestedReading,SuggestedMethods,UserId,Date,IsApproved")] Project project)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(project).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", project.UserId);
//            ViewBag.CauseId = new SelectList(db.Cause, "Id", "Description", project.CauseId);
//            ViewBag.SuitableSubjectId = new SelectList(db.SuitableSubject, "Id", "Description", project.SuitableSubjectId);
//            ViewBag.SuitableLevelId = new SelectList(db.SuitableLevel, "Id", "Description", project.SuitableLevelId);
//            return View(project);
//        }

//        // GET: Projects/Delete/5
//        [Authorize(Roles = "Admin")]
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Project project = db.Project.Find(id);
//            if (project == null)
//            {
//                return HttpNotFound();
//            }
//            return View(project);
//        }

//        // POST: Projects/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Project project = db.Project.Find(id);
//            db.Project.Remove(project);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
