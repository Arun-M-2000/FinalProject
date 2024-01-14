using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblDiagnosis
    {
        public TblDiagnosis()
        {
            TblPatientHistory = new HashSet<TblPatientHistory>();
        }

        public int DiagnosisId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Note { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<TblPatientHistory> TblPatientHistory { get; set; }
    }
}
