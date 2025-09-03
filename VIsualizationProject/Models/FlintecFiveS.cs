using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VIsualizationProject.Models
{
    public class FlintecFiveS : DbContext
    {
        public FlintecFiveS() : base("FiveSConnection") { }

        public DbSet<FiveS_Audit_Data> five_S { get; set; }
    }
}
