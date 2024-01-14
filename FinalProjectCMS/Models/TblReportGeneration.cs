using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblReportGeneration
    {
        public TblReportGeneration()
        {
            TblBillGeneration = new HashSet<TblBillGeneration>();
            TblPatientHistory = new HashSet<TblPatientHistory>();
        }

        public int ReportId { get; set; }
        public DateTime? ReportDate { get; set; }
        public string TestResult { get; set; }
        public string Remarks { get; set; }
        public int? AppointmentId { get; set; }
        public int? TestId { get; set; }
        public int? StaffId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual TblStaffs Staff { get; set; }
        public virtual TblLabTests Test { get; set; }
        public virtual ICollection<TblBillGeneration> TblBillGeneration { get; set; }
        public virtual ICollection<TblPatientHistory> TblPatientHistory { get; set; }
    }
}
