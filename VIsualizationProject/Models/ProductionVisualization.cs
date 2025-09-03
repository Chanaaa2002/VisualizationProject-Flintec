using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VIsualizationProject.Models
{
    public class ProductionVisualization : DbContext
    {
        internal object Announcement;
        internal IEnumerable<object> FiveS_Audit_Data;

        public ProductionVisualization() : base("DefaultConnection")
        {
        }
        public DbSet<UserVisual> UserVisuals { get; set; }
        public DbSet<Safety_Summary> Safety { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Birthdays> Birthdays { get; set; }
        public DbSet<OGQC> OGQCqu { get; set; }
        public DbSet<Layout> LayOut { get; set; }

    }
  
}