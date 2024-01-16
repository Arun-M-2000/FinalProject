using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Doctor;
using FinalProjectCMS.Repository.Receptionist;
using FinalProjectCMS.ViewModel.Receptionist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _appoinmentsRepository;
        // constructor injection

        public RAppointmentsController(IAppointmentsRepository AppoinmentsRepository)
        {
            _appoinmentsRepository = AppoinmentsRepository;
        }

        #region Listing All departments
        [HttpGet]
        [Route("List")]   // It is used in the case of not specifying the action

       
        public async Task<ActionResult<IEnumerable<TblDepartments>>> GetAllDepartment()
        {
            return await _appoinmentsRepository.GetAllDepartment();
        }
        #endregion

        #region Get all specialization by departmentId
        [HttpGet("GetSpecializationByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetAllSpecializaitonByDepartmentId(int? departmentId)
        {
            var specialization = await _appoinmentsRepository.GetAllSpecializationByDepartmentId(departmentId);
            if (specialization != null && specialization.Count > 0)
            {
                return Ok(specialization);
            }
            else
            {
                return NotFound();
            }

        }

        #endregion

        #region Get All Doctors by SpecializationId
        [HttpGet("GetDoctorBySpecializationId/{specializationId}")]
        public async Task<ActionResult<IEnumerable<DoctorViewModel>>> GetAllDoctorsBySpecializationId(int? specializationId)
        {
            var doctor = await _appoinmentsRepository.GetAllDoctorBySpecializationId(specializationId);
            if (doctor != null && doctor.Count > 0)
            {
                return Ok(doctor);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Book Appointment And Generate Bill
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment_ViewModel appointment_ViewModel, bool isNewPatient)
        {
            try
            {
                var bookNewAppointment = await _appoinmentsRepository.BookAppointment(appointment_ViewModel, isNewPatient);
                return Ok(bookNewAppointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Display  the Bill Details
        [HttpGet("GetBillDetails/{billId}")]
        public async Task<IActionResult> BillGeneration(int? billId)
        {
            try
            {
                var billgenerated = await _appoinmentsRepository.BillDetails(billId);
                if (billgenerated != null)
                {
                    return Ok(billgenerated);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        #endregion

        #region Get All Appointments
        [HttpGet("GetAllAppointments")]
        public async Task<IEnumerable<BillViewModel>> GetAllAppointments()
        {
            return await _appoinmentsRepository.GetAllAppointmentsWithBillViewModel();
        }
        #endregion

        #region Get AppointmentDetails by appointment Id
        [HttpGet("GetAppointment/{appointmentId}")]
        public async Task<ActionResult<BillViewModel>> GetAppointmentDetailsByAppointmentId(int? appointmentId)
        {
            try
            {
                var appointment = await _appoinmentsRepository.GetAppointmentDetailsById(appointmentId);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"error:{ex.Message}");

            }
        }
        #endregion

        #region CANCEL APPOINTMENT
        [HttpPatch("cancelAppointment/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int? appointmentId)
        {
            try
            {
                var appointment = await _appoinmentsRepository.CancelAppointment(appointmentId);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
        }
        #endregion

       

    }
}
