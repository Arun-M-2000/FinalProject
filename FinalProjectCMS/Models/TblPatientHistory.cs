using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblPatientHistory
    {
        public int PatientHistoryId { get; set; }
        public int? ReportId { get; set; }
        public int? DiagnosisId { get; set; }
        public int? MedPrescriptionId { get; set; }
        public int? LabPrescriptionId { get; set; }

        public virtual TblDiagnosis Diagnosis { get; set; }
        public virtual TblLabPrescriptions LabPrescription { get; set; }
        public virtual TblMedicinePrescriptions MedPrescription { get; set; }
        public virtual TblReportGeneration Report { get; set; }
    }
}
