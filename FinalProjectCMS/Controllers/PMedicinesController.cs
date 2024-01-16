using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Pharmacist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMedicinesController : ControllerBase
    {
        //data Filed 
        private readonly IMedicineRepository _medicineRepository;

        // construction Injection
        public PMedicinesController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        #region Get all Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMedicines>>> GetEmployeeAll()
        {
            return await _medicineRepository.GetAllTblMedicines();
        }
        #endregion

        #region GetEmployee By id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMedicines>> GetMedicineById(long? id)
        {
            try
            {
                var medicine = await _medicineRepository.GetMedicineById(id);
                if (medicine == null)
                {
                    return NotFound();
                }
                return Ok(medicine);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
