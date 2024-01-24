using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Pharmacist;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public class PatientMedRepository : IPatientMedRepository
    {
       
        private readonly ASPCMSDBContext _Context;

        public PatientMedRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }

        #region GetViewModelPatientsMedicine

        public async Task<IEnumerable<PatientMedViewModel>> GetViewModelPatientMed(int appointmentID)
        {
            var prescriptionList = await (from appointment in _Context.Appointment
                                          join prescription in _Context.TblMedicinePrescriptions on appointment.AppointmentId equals prescription.AppointmentId
                                          join patient in _Context.Patient on appointment.PatientId equals patient.PatientId
                                          join medicine in _Context.TblMedicines on prescription.PrescribedMedicineId equals medicine.MedicineId
                                          where appointment.AppointmentId == appointmentID
                                          select new PatientMedViewModel
                                          {
                                              RegNo = patient.RegisterNo,
                                              PatientName = patient.PatientName,
                                              PrescribedMedicine = medicine.MedicineName,
                                              Dosage=prescription.Dosage,
                                              DosageDays = (int)prescription.DosageDays,
                                              MedicineQuantity = (int)prescription.MedicineQuantity,
                                              UnitPrice = medicine.UnitPrice,
                                              AvailableStock = medicine.StockQuantity
                                          }).ToListAsync();

            return prescriptionList;
        }





        #endregion
    }
}
