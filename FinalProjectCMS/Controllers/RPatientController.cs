using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Receptionist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPatientController : ControllerBase
    {

        //data field

        private readonly  IPatientRepository _patientRepository;
        // constructor injection

        public RPatientController(IPatientRepository PatientRepository)
        {
            _patientRepository = PatientRepository;
        }

        #region Listing
        [HttpGet]
        [Route("List")]   // It is used in the case of not specifying the action

        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatient()
        {
            return await _patientRepository.GetAllPatient();
        }
        #endregion


        #region Adding
        [HttpPost]
        [Route("Insert")]

        public async Task<IActionResult> AddPatient([FromBody] Patient pa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var oid = await _patientRepository.AddPatient(pa);
                    if (oid > 0)
                    {
                        return Ok(oid);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion

        #region Updating

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdatePatient([FromBody] Patient pa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _patientRepository.UpdatePatient(pa);
                    return Ok(pa);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion



        #region Getpatient By id
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int? id)
        {
            try
            {
                var med = await _patientRepository.GetPatientById(id);
                if (med == null)
                {
                    return NotFound();
                }
                return Ok(med);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion


        #endregion

       
        #region Disable PatientRecords
        [HttpPatch("{patientId}")]
        public async Task<IActionResult> DisableStatus(int patientId)
        {
            try
            {
                var patient = await _patientRepository.DisableStatus(patientId);
                if (patient == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patient);
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Disable: {ex.Message}");
            }

        }
        #endregion

        #region Get All Disabled Patient Records

        [HttpGet]
        [Route("GetDisabledPatient")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetDisabledPatient()
        {
            return await _patientRepository.GetAllDisabledPatients();
        }
        #endregion

        #region Enable PatientRecords
        [HttpPatch("Enable/{patientId}")]
        public async Task<IActionResult> Enable(int patientId)
        {
            try
            {
                var patient = await _patientRepository.EnableStatus(patientId);
                if (patient == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patient);
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Enable: {ex.Message}");
            }

        }
        #endregion

    }
}
