using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Pharmacist;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Collections;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ASPCMSDBContext _Context;

        public PatientRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }



        #region GetViewModelPatients
        public async Task<IEnumerable<PatientViewModel>> GetPatientsWithMedicinePrescriptionsToday()
        {
            DateTime today = DateTime.Today;

            var patientList = await (
                from a in _Context.Appointment
                from p in _Context.Patient
                from d in _Context.TblDoctors
                from s in _Context.TblSpecializations
                from mp in _Context.TblMedicinePrescriptions
                from staff in _Context.TblStaffs // Assuming Staffs table for doctor names
                where a.AppointmentDate.Date == today
                      && a.PatientId == p.PatientId
                      && a.DocId == d.DocId
                      && d.SpecializationId == s.SpecializationId
                      && d.StaffId == staff.StaffId
                      && mp.AppointmentId == a.AppointmentId
                select new PatientViewModel
                {
                    RegNo = p.RegisterNo,
                    AppointmentId = a.AppointmentId,
                    MedPrescriptionId = mp.MedPrescriptionId,
                    PatientName = p.PatientName,
                    Age = CalculateAge(p.PatientDob),
                    Gender = p.Gender,
                    PhNo = p.PhoneNumber,
                    DoctorName = $"{staff.FullName} ({s.Specialization})"
                    // Other properties...
                }).ToListAsync();

            return patientList;
        }


        static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        #endregion



        #region GetPatientByRegNo
        public async Task<PatientViewModel> GetPatientByRegNo(string? RegNo)
        {
            if (_Context != null && RegNo != null)
            {
                var patientViewModel = await (
                    from p in _Context.Patient
                    from a in _Context.Appointment
                    from d in _Context.TblDoctors
                    from s in _Context.TblSpecializations
                    from staff in _Context.TblStaffs
                    where a.PatientId == p.PatientId && a.DocId == d.DocId && d.SpecializationId == s.SpecializationId && d.StaffId == staff.StaffId
                          && p.RegisterNo == RegNo
                    select new PatientViewModel
                    {
                        RegNo = p.RegisterNo,
                        PatientName = p.PatientName,
                        Age = CalculateAge(p.PatientDob),
                        Gender = p.Gender,
                        PhNo = p.PhoneNumber,
                        DoctorName = $"{staff.FullName} ({s.Specialization})",
                        // Other properties...
                    }).FirstOrDefaultAsync();

                return patientViewModel;
            }

            return null;
        }


        #endregion
    }


}
