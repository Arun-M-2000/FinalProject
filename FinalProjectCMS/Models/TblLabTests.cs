using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblLabTests
    {
        public TblLabTests()
        {
            TblLabPrescriptions = new HashSet<TblLabPrescriptions>();
            TblReportGeneration = new HashSet<TblReportGeneration>();
        }

        public int TestId { get; set; }
        public string TestName { get; set; }
        public string LowRange { get; set; }
        public string HighRange { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<TblLabPrescriptions> TblLabPrescriptions { get; set; }
        public virtual ICollection<TblReportGeneration> TblReportGeneration { get; set; }
    }
}
