using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VIsualizationProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<UserVisual> UserVisuals { get; set; }
        public DbSet<Safety_Summary> Safety { get; set; }
        public DbSet<Announcements> announcements { get; set; }
        public DbSet<Birthday1> birthday { get; set; }

    }
}