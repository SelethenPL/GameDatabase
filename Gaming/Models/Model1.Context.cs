﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gaming.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        [DbFunction("Entities", "raidsOnTheGo")]
        public virtual IQueryable<raidsOnTheGo_Result> raidsOnTheGo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<raidsOnTheGo_Result>("[Entities].[raidsOnTheGo]()");
        }

        public System.Data.Entity.DbSet<Gaming.Models.raidsOnTheGo_Result> raidsOnTheGo_Result { get; set; }
    }
}