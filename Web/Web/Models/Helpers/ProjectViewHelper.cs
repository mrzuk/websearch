using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.DAL;
using Web.Models.View;

namespace Web.Models.Helpers
{
    public static class ProjectHelpers
    {
        public static Project ViewToProject(this ProjectView data)
        {
            Project project = new Project();

            project.Title = data.Title;
            project.Date = (data.Date!=null)? (DateTime)data.Date : DateTime.UtcNow;
            project.Description = data.Description;
            project.Impact = data.Impact;
            project.IsApproved = data.IsApproved;
            project.ProjectArea = data.ProjectArea;
            project.Skills = data.Skills;
            project.SourceLink = data.SourceLink;
            project.SpecificProjects = data.SpecificProjects;
            project.SuggestedMethods = data.SuggestedMethods;
            project.SuggestedReading = data.SuggestedReading;
            project.Id = data.Id;
            project.AddedByEmail = data.UserEmail;
            project.AddedByName = data.UserName;

            List<Cause_Project> cause_projects = new List<Cause_Project>();
            foreach (int causeid in data.Causes)
            {
                cause_projects.Add(new Cause_Project() { CauseId = causeid, ProjectId = data.Id });
            }

            List<SuitableLevel_Project> suitableLevel_projects = new List<SuitableLevel_Project>();
            foreach (int suId in data.SuitableLevels)
            {
                suitableLevel_projects.Add(new SuitableLevel_Project() { SuitableLevelId = suId, ProjectId = data.Id });
            }

            List<SuitableSubjects_Project> suitableSubject_projects = new List<SuitableSubjects_Project>();
            foreach (int suId in data.SuitableSubjects)
            {
                suitableSubject_projects.Add(new SuitableSubjects_Project() { SuitableSubjectId = suId, ProjectId = data.Id });
            }

            //foreach (View.Cause c in data.Causes)
            //{
            //    cause_projects.Add(new Cause_Project() { CauseId = c.Id, ProjectId = data.Id });
            //}

            //List<SuitableLevel_Project> suitableLevel_projects = new List<SuitableLevel_Project>();
            //foreach (View.SuitableLevel suId in data.SuitableLevels)
            //{
            //    suitableLevel_projects.Add(new SuitableLevel_Project() { SuitableLevelId = suId.Id, ProjectId = data.Id });
            //}

            //List<SuitableSubjects_Project> suitableSubject_projects = new List<SuitableSubjects_Project>();
            //foreach (View.SuitableSubject suId in data.SuitableSubjects)
            //{
            //    suitableSubject_projects.Add(new SuitableSubjects_Project() { SuitableSubjectId = suId.Id, ProjectId = data.Id });
            //}

            project.Cause_Project = cause_projects;
            project.SuitableLevel_Project = suitableLevel_projects;
            project.SuitableSubjects_Project = suitableSubject_projects;

            return project;
        }

        public static ProjectView ProjectToProjectView(this Project data)
        {
            ProjectView view = new ProjectView();

            view.Title = data.Title;
            view.Date = data.Date;
            view.Description = data.Description;
            view.Impact = data.Impact;
            view.IsApproved = data.IsApproved;
            view.ProjectArea = data.ProjectArea;
            view.Skills = data.Skills;
            view.SourceLink = data.SourceLink;
            view.SpecificProjects = data.SpecificProjects;
            view.SuggestedMethods = data.SuggestedMethods;
            view.SuggestedReading = data.SuggestedReading;
            view.Id = data.Id;
            view.UserName = data.AddedByName;
            view.UserEmail = data.AddedByEmail;
           
            view.CauseCollection = data.Cause_Project.ToList().Select(a => new View.Cause() { Id = a.CauseId, Description = a.Cause.Description }).ToList();
            view.LevelCollection = data.SuitableLevel_Project.ToList().Select(a => new View.SuitableLevel() { Id = a.SuitableLevelId, Description = a.SuitableLevel.Description }).ToList();
            view.SubjectCollection = data.SuitableSubjects_Project.ToList().Select(a => new View.SuitableSubject() { Id = a.SuitableSubjectId, Description = a.SuitableSubject.Description }).ToList();


            view.SuitableSubjects = view.SubjectCollection.Select(a => a.Id).ToList();
            view.Causes = view.CauseCollection.Select(a => a.Id).ToList();
            view.SuitableLevels = view.LevelCollection.Select(a => a.Id).ToList();

            view.InterestedUsers = data.InterestedUsers_Projects.Where(c=> c.ProjectId==data.Id).Select(iup => iup.UserEmail).ToList();
            //view.SuitableSubjects = data.SuitableSubjects_Project.ToList().Select(a => a.SuitableSubjectId).ToList();
            //view.Causes = data.Cause_Project.ToList().Select(a => a.CauseId).ToList();
            //view.SuitableLevels = data.SuitableLevel_Project.ToList().Select(a => a.SuitableLevelId).ToList();

            //view.SuitableSubjects = data.SuitableSubjects_Project.ToList().Select(a => new View.SuitableSubject() { Id = a.SuitableSubjectId, Description = a.SuitableSubject.Description }).ToList();
            //view.Causes = data.Cause_Project.ToList().Select(a => new View.Cause() { Id = a.CauseId, Description = a.Cause.Description }).ToList();
            //view.SuitableLevels = data.SuitableLevel_Project.ToList().Select(a => new View.SuitableLevel() { Id = a.Id, Description = a.SuitableLevel.Description }).ToList();

            return view;

        }
    }
}