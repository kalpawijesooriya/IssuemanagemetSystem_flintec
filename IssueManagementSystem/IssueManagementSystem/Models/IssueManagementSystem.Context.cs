﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IssueManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class issue_management_systemEntities1 : DbContext
    {
        public issue_management_systemEntities1()
            : base("name=issue_management_systemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<display> displays { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<issue_line_person> issue_line_person { get; set; }
        public virtual DbSet<issue_occurrence> issue_occurrence { get; set; }
        public virtual DbSet<issue> issues { get; set; }
        public virtual DbSet<line_computer> line_computer { get; set; }
        public virtual DbSet<line_map> line_map { get; set; }
        public virtual DbSet<line_supervisor> line_supervisor { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_tbl> User_tbl { get; set; }
        public virtual DbSet<line_machine> line_machine { get; set; }
        public virtual DbSet<line> lines { get; set; }
        public virtual DbSet<machine> machines { get; set; }
        public virtual DbSet<material> materials { get; set; }
    }
}
