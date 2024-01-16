using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Pharmacist;
using FinalProjectCMS.ViewModel.Pharmacist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PPatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PPatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        #region Get ViewModel Patients
        [HttpGet]
        [Route("ViewModelGetPatientsList")]

        public async Task<ActionResult<IEnumerable<PatientViewModel>>> GetPatientList()
        {
            try
            {
                var patientList = await _patientRepository.GetViewModelPatient();
                return Ok(patientList);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        #endregion

        
        #region Search Patient by RegNo

        [HttpGet("{RegNo}")]
        public async Task<ActionResult<PatientViewModel>> GetPatientByRegNo(string? RegNo)
        {
            try
            {
                var patientViewModel = await _patientRepository.GetPatientByRegNo(RegNo);
                if (patientViewModel == null)
                {
                    return NotFound();
                }

                return Ok(patientViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
