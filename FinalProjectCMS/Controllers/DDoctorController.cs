using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Doctor;
using FinalProjectCMS.ViewModel.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DDoctorController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public readonly IPatientDetailsRepository _patientDetailsRepository;
        public readonly IPatientHistoryRepository _patientHistoryRepository;
        public readonly IDiagnosisRepository _diagnosisRepository;
        private readonly ILogger<DDoctorController> _logger;

        public DDoctorController(IAppointmentRepository appointmentRepository, IPatientDetailsRepository patientDetailsRepository, IPatientHistoryRepository patientHistoryRepository, IDiagnosisRepository diagnosisRepository, ILogger<DDoctorController> logger)
        {
            _appointmentRepository = appointmentRepository;
            _patientDetailsRepository = patientDetailsRepository;
            _patientHistoryRepository = patientHistoryRepository;
            _diagnosisRepository= diagnosisRepository;
            _logger = logger;
        }

        [HttpGet("GetAppointmentView/{docId}")]
        public async Task<ActionResult<IEnumerable<AppointmentsVM>>> GetAppointmentView(int docId)
        {
            var appointments = await _appointmentRepository.GetAppointmentViewAsync(docId);

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(appointments);
        }

        [HttpGet("GetPatientView/{appointmentId}")]
        public async Task<ActionResult<PatientDetails>> GetPatientView(int appointmentId)
        {
            var patientDetails = await _patientDetailsRepository.GetPatientViewAsync(appointmentId);

            if (patientDetails == null)
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(patientDetails);
        }

        [HttpGet("GetPatientHistory/{patientId}")]
        public async Task<ActionResult<IEnumerable<PatientHistory>>> GetPatientHistory(int patientId)
        {
            var patientHistory = await _patientHistoryRepository.GetPatientHistoryAsync(patientId);

            if (patientHistory == null || !patientHistory.Any())
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(patientHistory);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiagnosis([FromBody] DiagnosisVM diagnosis)
        {
            //check the validation of the body
            if (ModelState.IsValid)
            {
                try
                {
                    var diagId = await _diagnosisRepository.FillDiagForm(diagnosis);
                    if (diagId > 0)
                    {
                        return Ok(diagId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex.StackTrace);
                    return BadRequest();
                }
            }
            return BadRequest();
        }







    }
}
