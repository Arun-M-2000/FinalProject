using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblLabPrescriptions
    {
        public TblLabPrescriptions()
        {
            TblPatientHistory = new HashSet<TblPatientHistory>();
        }

        public int LabPrescriptionId { get; set; }
        public int? LabTestId { get; set; }
        public string LabNote { get; set; }
        public string LabTestStatus { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual TblLabTests LabTest { get; set; }
        public virtual ICollection<TblPatientHistory> TblPatientHistory { get; set; }
    }
}
