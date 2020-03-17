using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MGSProject.Models
{
    public class TempMainDBContext : MainDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Repository>()
                    .Property(a => a.RepositoryId)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                base.OnModelCreating(modelBuilder);
            }
        }
    
}