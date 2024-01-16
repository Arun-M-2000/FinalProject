using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Doctor;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;


namespace FinalProjectCMS.Repository.Doctor
{
    public class PatientDetailsRepository: IPatientDetailsRepository
    {
        private readonly ASPCMSDBContext _aSPCMSDBContext;
        public PatientDetailsRepository(ASPCMSDBContext aSPCMSDBContext)
        {
            _aSPCMSDBContext = aSPCMSDBContext;
        }

        public async Task<PatientDetails> GetPatientViewAsync(int appointmentId)
        {
            using (var context = new ASPCMSDBContext()) // Replace YourDbContext with the actual name of your DbContext
            {
                var query = from patient in context.Patient
                            join appointment in context.Appointment on patient.PatientId equals appointment.PatientId
                            where appointment.AppointmentId == appointmentId
                            select new PatientDetails
                            {
                                Appointment_Id = appointment.AppointmentId,
                                Patient_Name = patient.PatientName,
                                Gender = patient.Gender,
                                BloodGroup = patient.BloodGroup,
                                PhoneNumber = patient.PhoneNumber,
                                AppointmentStatus = appointment.CheckUpStatus,
                                PatientAge = (int)Math.Floor((DateTime.Now - patient.PatientDob).TotalDays / 365)
                            };

                return await query.FirstOrDefaultAsync();
            }
        }




    }
}
