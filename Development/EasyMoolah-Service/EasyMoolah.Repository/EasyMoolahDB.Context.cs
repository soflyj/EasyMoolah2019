﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyMoolah.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EasyMoolahEntities : DbContext
    {
        public EasyMoolahEntities()
            : base("name=EasyMoolahEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<NotificationLog> NotificationLogs { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<APILog> APILogs { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
    }
}
