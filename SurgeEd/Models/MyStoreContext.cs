using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SurgeEd.Models
{
    public class MyStoreContext: DbContext
    {
        public DbSet<Document> Document { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .ToTable("Document")
                .HasKey(e => e.DocumentId);

          
        }


    }
}