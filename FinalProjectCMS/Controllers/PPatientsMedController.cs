using FinalProjectCMS.Repository.Pharmacist;
using FinalProjectCMS.ViewModel.Pharmacist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PPatientsMedController : ControllerBase
    {
        private readonly IPatientMedRepository _patientMedRepository;

        public PPatientsMedController(IPatientMedRepository patientMedRepository)
        {
            _patientMedRepository = patientMedRepository;
        }

        #region Get ViewModel Patients
        [HttpGet]
        [Route("ViewModelGetPatientsMedList")]

        public async Task<ActionResult<IEnumerable<PatientMedViewModel>>> GetPatientMedViewModel()
        {
            try
            {
                var patientMedList = await _patientMedRepository.GetViewModelPatientMed();
                return Ok(patientMedList);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        #endregion


    }
}

