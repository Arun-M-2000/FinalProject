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
    public class ALabController : ControllerBase
    {
        private readonly ILabRepository _labRepository;

        // construction Injection
        public ALabController(ILabRepository labRepository)
        {
            _labRepository = labRepository;
        }
        #region GEt all LabTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblLabTests>>> GetEmployeeAll()
        {
            return await _labRepository.GetAllTblLabTests();
        }
        #endregion

        #region Add Labtest
        [HttpPost]
        public async Task<IActionResult> AddLab([FromBody] TblLabTests lab)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    var labId = await _labRepository.AddLabtest(lab);
                    if (labId > 0)
                    {
                        return Ok(labId);
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



        #region Update LabTest
        [HttpPut]
        public async Task<IActionResult> UpdateLabTest([FromBody] TblLabTests lab)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    await _labRepository.UpdateLabTest(lab);

                    return Ok(lab);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion


        #region GetEmployee By id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblLabTests>> GetlabById(int? id)
        {
            try
            {
                var lab = await _labRepository.GetLabById(id);
                if (lab == null)
                {
                    return NotFound();
                }
                return Ok(lab);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion


        #region Delete an Employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLab(int? id)
        {
            try
            {
                var labId = await _labRepository.DeleteLabTest(id);
                if (labId > 0)
                {
                    return Ok(labId);
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
