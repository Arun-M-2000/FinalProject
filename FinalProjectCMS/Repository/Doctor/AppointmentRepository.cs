using FinalProjectCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using FinalProjectCMS.ViewModel.Doctor;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCMS.Repository.Doctor
{
    public class AppointmentRepository:IAppointmentRepository
    {
        private readonly ASPCMSDBContext _aSPCMSDBContext;
        public AppointmentRepository(ASPCMSDBContext aSPCMSDBContext)
        {
            _aSPCMSDBContext = aSPCMSDBContext;
        }

        # region GetPatientAppointments

        public async Task<List<AppointmentsVM>> GetAppointmentViewAsync(int docId)
        {
            using (var context = new ASPCMSDBContext()) // Replace YourDbContext with the actual name of your DbContext
            {
                var todayDate = DateTime.Now.Date;

                var query = from patient in context.Patient
                            join appointment in context.Appointment on patient.PatientId equals appointment.PatientId
                            where appointment.DocId == docId && appointment.AppointmentDate.Date == todayDate
                            select new AppointmentsVM
                            {
                                Token_No = appointment.TokenNo,
                                Patient_Name = patient.PatientName,
                                Gender = patient.Gender,
                                PatientAge = (int)Math.Floor((todayDate - patient.PatientDob).TotalDays / 365),
                                CheckUp_Status = appointment.CheckUpStatus,
                                Appointment_Id = appointment.AppointmentId,
                                Appointment_Date= DateTime.Now.Date,
                                Doc_Id=docId
                            };

                return await query.ToListAsync();
            }
        }

        #endregion





    }
}
