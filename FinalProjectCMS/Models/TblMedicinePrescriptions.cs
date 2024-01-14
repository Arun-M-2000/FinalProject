using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblMedicinePrescriptions
    {
        public TblMedicinePrescriptions()
        {
            TblPatientHistory = new HashSet<TblPatientHistory>();
        }

        public int MedPrescriptionId { get; set; }
        public long? PrescribedMedicineId { get; set; }
        public string Dosage { get; set; }
        public int? DosageDays { get; set; }
        public int? MedicineQuantity { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual TblMedicines PrescribedMedicine { get; set; }
        public virtual ICollection<TblPatientHistory> TblPatientHistory { get; set; }
    }
}
