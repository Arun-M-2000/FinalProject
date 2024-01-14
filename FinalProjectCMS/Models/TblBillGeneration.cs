using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblBillGeneration
    {
        public int BillId { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ReportId { get; set; }

        public virtual TblReportGeneration Report { get; set; }
    }
}
