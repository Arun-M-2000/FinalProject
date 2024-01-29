using FinalProjectCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FinalProjectCMS.ViewModel.Doctor;
using Microsoft.EntityFrameworkCore;
using System;



namespace FinalProjectCMS.Repository.Doctor
{
    public class PatientHistoryRepository: IPatientHistoryRepository
    {

        private readonly ASPCMSDBContext _aSPCMSDBContext;
        public PatientHistoryRepository(ASPCMSDBContext aSPCMSDBContext)
        {
            _aSPCMSDBContext = aSPCMSDBContext;
        }
        public async Task<List<PatientHistory>> GetPatientHistoryAsync(int patientId)
        {
          
            if (_aSPCMSDBContext != null)
            {
                var patientHistory = await _aSPCMSDBContext.Appointment
             .Include(a => a.TblDiagnosis)
                 .ThenInclude(d => d.TblPatientHistory)
             .Include(a => a.TblLabPrescriptions)
                 .ThenInclude(lp => lp.LabTest)
             .Include(a => a.TblMedicinePrescriptions)
                 .ThenInclude(mp => mp.PrescribedMedicine)
             .Include(a => a.TblReportGeneration)
                 .ThenInclude(rg => rg.Test)
             .Where(a => a.PatientId == patientId)
             .OrderByDescending(a => a.AppointmentDate)
             .Select(a => new PatientHistory
             {
                 appointment_Date = a.AppointmentDate,
                 symptoms = a.TblDiagnosis.FirstOrDefault().Symptoms,
                 diagnosis_Desc = a.TblDiagnosis.FirstOrDefault().Diagnosis,
                 doc_notes = a.TblDiagnosis.FirstOrDefault().Note,
                 medpres_Name = a.TblMedicinePrescriptions.FirstOrDefault().PrescribedMedicine.MedicineName,
                 labtest_Name = a.TblLabPrescriptions.FirstOrDefault().LabTest.TestName,
                 lab_Result = a.TblReportGeneration.FirstOrDefault().TestResult ?? "N.A"
             })
             .ToListAsync();

                return patientHistory;



            }
            return null;

        }



    }
}
