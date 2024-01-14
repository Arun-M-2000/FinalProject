using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            ConsultBill = new HashSet<ConsultBill>();
            TblDiagnosis = new HashSet<TblDiagnosis>();
            TblLabPrescriptions = new HashSet<TblLabPrescriptions>();
            TblMedicinePrescriptions = new HashSet<TblMedicinePrescriptions>();
            TblReportGeneration = new HashSet<TblReportGeneration>();
        }

        public int AppointmentId { get; set; }
        public int TokenNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DocId { get; set; }
        public string CheckUpStatus { get; set; }

        public virtual TblDoctors Doc { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<ConsultBill> ConsultBill { get; set; }
        public virtual ICollection<TblDiagnosis> TblDiagnosis { get; set; }
        public virtual ICollection<TblLabPrescriptions> TblLabPrescriptions { get; set; }
        public virtual ICollection<TblMedicinePrescriptions> TblMedicinePrescriptions { get; set; }
        public virtual ICollection<TblReportGeneration> TblReportGeneration { get; set; }
    }
}
