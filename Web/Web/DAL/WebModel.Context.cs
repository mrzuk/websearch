﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebDbEntities : DbContext
    {
        public WebDbEntities()
            : base("name=WebDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Cause> Cause { get; set; }
        public virtual DbSet<SuitableSubject> SuitableSubject { get; set; }
        public virtual DbSet<SuitableLevel> SuitableLevel { get; set; }
        public virtual DbSet<Cause_Project> Cause_Project { get; set; }
        public virtual DbSet<SuitableLevel_Project> SuitableLevel_Project { get; set; }
        public virtual DbSet<SuitableSubjects_Project> SuitableSubjects_Project { get; set; }
        public virtual DbSet<Configurations> Configurations { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<InterestedUsers_Projects> InterestedUsers_Projects { get; set; }
    }
}
