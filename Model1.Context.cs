﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SQLTARGIL4
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FaceMinistryEntities : DbContext
    {
        public FaceMinistryEntities()
            : base("name=FaceMinistryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CITy> CITIES { get; set; }
        public virtual DbSet<DISTRICT> DISTRICTS { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
