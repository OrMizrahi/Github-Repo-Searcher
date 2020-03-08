using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MGSProject.Models
{
    public class MainDbContext : DbContext
    {
        public DbSet<Repository> Repositories { get; set; }
    }
}