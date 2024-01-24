using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Doctor;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public class DiagnosisRepository: IDiagnosisRepository
    {
        private readonly ASPCMSDBContext _aSPCMSDBContext;

        public DiagnosisRepository(ASPCMSDBContext aSPCMSDBContext)
        {
            _aSPCMSDBContext = aSPCMSDBContext;
        }

        public async Task<int?> FillDiagForm(DiagnosisVM diagnosisVM)
        {
            var diagnosis = new TblDiagnosis
            {
                Symptoms = diagnosisVM.Symptoms ?? "",
                Diagnosis = diagnosisVM.Diagnosis1 ?? "",
                Note = diagnosisVM.Note ?? "",
                AppointmentId = diagnosisVM.AppointmentId
            };

            _aSPCMSDBContext.TblDiagnosis.Add(diagnosis);
            await _aSPCMSDBContext.SaveChangesAsync();

            // Insert into tbl_Medicineprescription
            var medicinePrescription = new TblMedicinePrescriptions
            {
                PrescribedMedicineId = diagnosisVM.PrescribedMedicineId,
                Dosage = diagnosisVM.Dosage ?? "",
                DosageDays = diagnosisVM.DosageDays,
                MedicineQuantity = diagnosisVM.MedicineQuantity,
                AppointmentId = diagnosisVM.AppointmentId
            };

            _aSPCMSDBContext.TblMedicinePrescriptions.Add(medicinePrescription);
            await _aSPCMSDBContext.SaveChangesAsync();

            // Insert into tbl_LabPrescription
            var labPrescription = new TblLabPrescriptions
            {
                LabTestId = diagnosisVM.LabTestId,
                LabNote = diagnosisVM.LabNote ?? "",
                AppointmentId = diagnosisVM.AppointmentId,
                LabTestStatus = diagnosisVM.LabTestStatus
            };

            _aSPCMSDBContext.TblLabPrescriptions.Add(labPrescription);
            await _aSPCMSDBContext.SaveChangesAsync();

            return diagnosisVM.AppointmentId;
        }


    }
}
