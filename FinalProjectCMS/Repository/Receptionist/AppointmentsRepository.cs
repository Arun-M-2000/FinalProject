using FinalProjectCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Linq;
using FinalProjectCMS.ViewModel.Receptionist;
using System;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FinalProjectCMS.Repository.Receptionist
{
    public class AppointmentsRepository:IAppointmentsRepository
    {
        private readonly ASPCMSDBContext _dbContext;


        public AppointmentsRepository(ASPCMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region get all departments

        //Get all Departments

        public async Task<List<TblDepartments>> GetAllDepartment()
        {
            if (_dbContext != null)
            {
                return await _dbContext.TblDepartments.ToListAsync();
            }
            return null;
        }
        #endregion

        
        #region Get all Specialization By Department Id
        public async Task<List<TblSpecializations>> GetAllSpecializationByDepartmentId(int? departmentId)
        {
            if (_dbContext != null)
            {
                return await _dbContext.TblSpecializations.Where(s => s.DepartmentId == departmentId).ToListAsync();
            }
            return null;
        }
        #endregion

        #region Get Doctor by Specializaton Id
        public async Task<List<DoctorViewModel>> GetAllDoctorBySpecializationId(int? specializationId)
        {
            if (_dbContext != null)
            {
                var doctors = await _dbContext.TblDoctors
            .Where(d => d.SpecializationId == specializationId)
            .Include(d => d.Staff)
            .Include(d => d.Staff.Specialization)
            .Select(d => new DoctorViewModel
            {
                DocId = d.DocId,
                ConsultationFee = d.ConsultationFee,
                StaffId = d.StaffId,
                SpecializationId = d.SpecializationId,
                QualificationId = d.Staff.QualificationId,
                LoginId = d.Staff.LoginId,
                FullName = d.Staff.FullName,
                Dob = d.Staff.Dob,
                Gender = d.Staff.Gender,
               
                BloodGroup = d.Staff.BloodGroup,
                JoiningDate = d.Staff.JoiningDate,
                Salary = d.Staff.Salary,
                MobileNo = d.Staff.MobileNo,
                DepartmentId = d.Staff.DepartmentId,
                Email = d.Staff.Email,
                RoleId = d.Staff.RoleId
            })
            .ToListAsync();

                return doctors;

            }
            return null;
        }
        #endregion

        #region Book Appointment and Bill Genereation
        public async Task<Appointment_ViewModel> BookAppointment(Appointment_ViewModel viewModel, bool isNewPatient)
        {
            var existingAppointment = await _dbContext.Appointment.Where(a => a.PatientId == viewModel.PatientId
            && a.DocId == viewModel.DocId && a.AppointmentDate == viewModel.AppointmentDate).FirstOrDefaultAsync();
            if (existingAppointment != null)
            {
                throw new InvalidOperationException("Appointment already exists for the same doctor and same date");
            }
            if (_dbContext != null)
            {
                var lastTokenNumber = await _dbContext.Appointment.Where(a => a.DocId == viewModel.DocId && a.AppointmentDate == viewModel.AppointmentDate).OrderByDescending(a => a.TokenNo).Select(a => a.TokenNo).FirstOrDefaultAsync();

                int newTokenNumber;
                if (lastTokenNumber > -1 && lastTokenNumber < 100)
                {
                    newTokenNumber = lastTokenNumber + 1;
                }
                else
                {
                    throw new InvalidOperationException("No more token is available for today");
                }
                viewModel.TokenNo = newTokenNumber;
                var newAppointment = new Appointment()
                {
                    TokenNo = viewModel.TokenNo,
                    PatientId = viewModel.PatientId,
                    AppointmentDate = viewModel.AppointmentDate,
                    DocId = viewModel.DocId,

                };
                _dbContext.Appointment.Add(newAppointment);
                await _dbContext.SaveChangesAsync();

                viewModel.AppointmentId = newAppointment.AppointmentId;
                viewModel.CheckUpStatus = viewModel.CheckUpStatus;
                decimal registerFee;
                if (isNewPatient)
                {
                    registerFee = viewModel.RegisterFees ?? 150;

                }
                else
                {
                    registerFee = viewModel.RegisterFees ?? 0;
                }

                decimal consultFees = viewModel.ConsultationFee ?? _dbContext.TblDoctors.Where(d => d.DocId == viewModel.DocId).Select(d => (decimal?)d.ConsultationFee).FirstOrDefault() ?? 0;
                decimal totalAmount = registerFee + consultFees + (0.18m * registerFee) + (0.18m * consultFees);

                viewModel.RegisterFees = registerFee;
                viewModel.ConsultationFee = (int?)consultFees;
                viewModel.TotalAmount = totalAmount;
                var newConsultBill = new ConsultBill()
                {
                    AppointmentId = viewModel.AppointmentId,
                    RegisterFees = registerFee,
                    TotalAmt = totalAmount
                };


                _dbContext.ConsultBill.Add(newConsultBill);
                await _dbContext.SaveChangesAsync();
                viewModel.BillId = newConsultBill.BillId;
                return viewModel;
            }
            return null;
        }
        #endregion

        #region Display Bill Details
        public async Task<BillViewModel> BillDetails(int? billId)
        {
            if (_dbContext == null)
                return null;

            var bill = await _dbContext.ConsultBill.FindAsync(billId);
            if (bill == null)
                return null;

            var appointment = await GetAppointmentDetails(bill.AppointmentId);
            if (appointment == null)
                return null;

            var patient = await GetPatientDetails(appointment.PatientId);
            var doctor = await GetDoctorDetails(appointment.DocId);
            if (doctor == null)
                return null;

            var specialization = await GetSpecializationDetails(doctor.SpecializationId);
            var department = await GetDepartmentDetails(specialization.DepartmentId);
            var staff = await GetStaffDetails(doctor.StaffId);

            if (staff == null || specialization == null || department == null)
                return null;

            return CreateBillViewModel(patient, doctor, staff, specialization, department, appointment, bill);
        }

        private async Task<Appointment> GetAppointmentDetails(int? appointmentId)
        {
            return await _dbContext.Appointment.FindAsync(appointmentId);
        }

        private async Task<Patient> GetPatientDetails(int? patientId)
        {
            return await _dbContext.Patient.FindAsync(patientId);
        }

        private async Task<TblDoctors> GetDoctorDetails(int? doctorId)
        {
            return await _dbContext.TblDoctors.FindAsync(doctorId);
        }

        private async Task<TblSpecializations> GetSpecializationDetails(int? specializationId)
        {
            return await _dbContext.TblSpecializations.FindAsync(specializationId);
        }

        private async Task<TblDepartments> GetDepartmentDetails(int? departmentId)
        {
            return await _dbContext.TblDepartments.FindAsync(departmentId);
        }

        private async Task<TblStaffs> GetStaffDetails(int? staffId)
        {
            return await _dbContext.TblStaffs.FindAsync(staffId);
        }

        private BillViewModel CreateBillViewModel(Patient patient, TblDoctors doctor, TblStaffs staff, TblSpecializations specialization, TblDepartments department, Appointment appointment, ConsultBill bill)
        {
            return new BillViewModel()
            {
                PatientId = patient.PatientId,
                PatientName = patient.PatientName,
                RegisterNo = patient.RegisterNo,
                DocId = doctor.DocId,
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                RegisterFees = bill.RegisterFees,
                ConsultationFee = doctor.ConsultationFee,
                SpecializationId = specialization.SpecializationId,
                TotalAmt = bill.TotalAmt,
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                AppointmentId = appointment.AppointmentId,
                TokenNo = appointment.TokenNo,
                AppointmentDate = appointment.AppointmentDate,
                CheckUpStatus = appointment.CheckUpStatus,
                BillId = bill.BillId,
            };
        }

        #endregion

        #region Get All Appointments with BillViewModel
        public async Task<List<BillViewModel>> GetAllAppointmentsWithBillViewModel()
        {
            if (_dbContext != null)
            {
                var appointments = await _dbContext.Appointment
             .Include(a => a.Patient)
             .Include(a => a.Doc).ThenInclude
             (d => d.Staff).Include(a => a.Doc).ThenInclude(d => d.Specialization).ThenInclude(s => s.Department).Where(a => a.CheckUpStatus == "CONFIRMED")
             .ToListAsync();
                // Enable logging to console
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                        .AddConsole();
                });

               /* _dbContext.Database.SetCommandTimeout(180);
                _dbContext.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());*/

                // Your actual query to retrieve Staff with Department
                var staffWithDepartment = await _dbContext.TblStaffs
                    .Include(s => s.Department)
                    .FirstOrDefaultAsync(s => s.DepartmentId == s.Department.DepartmentId);

              
               
                var appointmentsWithViewModel = new List<BillViewModel>();

                foreach (var appointment in appointments)
                {
                    // Find the ConsultBill for the current appointment
                    var consultBill = await _dbContext.ConsultBill
                        .FirstOrDefaultAsync(b => b.AppointmentId == appointment.AppointmentId);

                    // Transform Appointment entity and ConsultBill to BillViewModel
                    var appointmentWithViewModel = new BillViewModel
                    {
                        AppointmentId = appointment.AppointmentId,
                        TokenNo = appointment.TokenNo,
                        AppointmentDate = appointment.AppointmentDate,
                        PatientId = appointment.PatientId,
                        DocId = appointment.DocId,
                        CheckUpStatus = appointment.CheckUpStatus,
                        BillId = consultBill.BillId,
                        RegisterFees = consultBill?.RegisterFees,
                        TotalAmt = consultBill.TotalAmt,
                        ConsultationFee = appointment.Doc?.ConsultationFee,
                        StaffId = appointment.Doc?.StaffId,
                        SpecializationId = appointment.Doc?.SpecializationId,
                        DepartmentId = (int)(appointment.Doc?.Specialization?.DepartmentId),
                        DepartmentName = appointment.Doc?.Specialization?.Department?.DepartmentName,
                        FullName = appointment.Doc?.Staff?.FullName,
                        RegisterNo = appointment.Patient?.RegisterNo,
                        PatientName = appointment.Patient?.PatientName
                    };

                    appointmentsWithViewModel.Add(appointmentWithViewModel);
                }


                return appointmentsWithViewModel;
            }
            return null;
        }
        #endregion

        #region Get the appointment details by Appointment Id
        public async Task<BillViewModel> GetAppointmentDetailsById(int? appointmentId)
        {
            if (_dbContext != null)
            {
                var appointments = await _dbContext.Appointment
                    .Include(a => a.Patient)
                    .Include(a => a.Doc).ThenInclude
                    (d => d.Staff).Include(a => a.Doc).ThenInclude(d => d.Specialization).ThenInclude(s => s.Department).Where(a => a.CheckUpStatus == "CONFIRMED").FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

                var ConsultBill = await _dbContext.ConsultBill.FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);
                var billViewModel = new BillViewModel
                {
                    AppointmentId = appointments.AppointmentId,
                    TokenNo = appointments.TokenNo,
                    AppointmentDate = appointments.AppointmentDate,
                    PatientId = appointments.PatientId,
                    DocId = appointments.DocId,
                    CheckUpStatus = appointments.CheckUpStatus,
                    BillId = ConsultBill.BillId,
                    RegisterFees = ConsultBill?.RegisterFees,
                    TotalAmt = ConsultBill.TotalAmt,
                    ConsultationFee = appointments.Doc?.ConsultationFee,
                    StaffId = appointments.Doc?.StaffId,
                    SpecializationId = appointments.Doc?.SpecializationId,
                    DepartmentId = appointments.Doc?.Specialization?.DepartmentId,
                    DepartmentName = appointments.Doc?.Specialization?.Department?.DepartmentName,
                    FullName = appointments.Doc?.Staff?.FullName,
                    RegisterNo = appointments.Patient?.RegisterNo,
                    PatientName = appointments.Patient?.PatientName
                };
                return billViewModel;
            }
            return null;
        }
        #endregion

        #region Cancel Appointment
        public async Task<Appointment> CancelAppointment(int? appointmentId)
        {
            if (_dbContext != null)
            {
                var appointment = await _dbContext.Appointment.FindAsync(appointmentId);
                if (appointment != null)
                {
                    appointment.CheckUpStatus = "CANCELLED";
                    await _dbContext.SaveChangesAsync();

                }
                return appointment;
            }
            return null;
        }
        #endregion



       


    }
}
