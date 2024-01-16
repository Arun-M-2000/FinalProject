using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AMedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;

        // construction Injection
        public AMedicineController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        #region GEt all Medicine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMedicines>>> GetMedicineAll()
        {
            return await _medicineRepository.GetAllTblMedicines();
        }
        #endregion


        #region Add Medicine
        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromBody] TblMedicines medicine)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    var medId = await _medicineRepository.AddMedicine(medicine);
                    if (medId > 0)
                    {
                        return Ok(medId);
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



        #region Update Medicine
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] TblMedicines med)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    await _medicineRepository.UpdateMedicine(med);

                    return Ok(med);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion


        #region GetMedicine By id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMedicines>> GetMedicineById(int? id)
        {
            try
            {
                var med = await _medicineRepository.GetMedicineById(id);
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


        #region Delete an Medicine
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int? id)
        {
            try
            {
                var medId = await _medicineRepository.DeleteMedicine(id);
                if (medId > 0)
                {
                    return Ok(medId);
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
        #endregion
    }
}
