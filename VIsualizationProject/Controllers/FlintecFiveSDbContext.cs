using System;

namespace VIsualizationProject.Controllers
{
    internal class FlintecFiveSDbContext : IDisposable
    {
        public object FiveSAuditDatas { get; internal set; }
        public object FiveSAggregateView { get; internal set; }
        public object FiveS_Audit_Data { get; internal set; }
        public object Flintec_FiveS { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}