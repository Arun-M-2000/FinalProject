using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalProjectCMS.ViewModel.Admin;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AStaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        // construction Injection
        public AStaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;

        }

        #region Update Staff
        [HttpPut]
        public async Task<IActionResult> UpdateStaff([FromBody] TblStaffs staff)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    await _staffRepository.UpdateStaff(staff);

                    return Ok(staff);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

      

        #region Delete an Staff
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int? id)
        {
            try
            {
                var employeeId = await _staffRepository.DeleteStaff(id);
                if (employeeId > 0)
                {
                    return Ok(employeeId);
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








        #region Get ViewModel Employees
        [HttpGet]

        public async Task<ActionResult<IEnumerable<StaffDetailsViewModel>>> GetStaffs()
        {
            return await _staffRepository.GetStaffDetails();
        }

        #endregion





        #region GetStaff By id
        [HttpGet("{staffId}")]
        public async Task<ActionResult<StaffDetailsViewModel>> GetStaffDetailsById(int? staffId)
        {
            try
            {
                var staffDetails = await _staffRepository.GetStaffDetailsById(staffId);

                if (staffDetails == null)
                {
                    return NotFound(); // Staff not found, return 404 Not Found
                }

                return Ok(staffDetails); // Return staff details with 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Consider returning a more specific status code or message
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        #endregion



        //--------------Adding a staff-----------------


        [HttpPost]

        public async Task<IActionResult> AddStaffWithRelatedData([FromBody] StaffDetailsViewModel staffDetails)
        {
            try
            {
                var StaffId = await _staffRepository.AddStaffWithRelatedData(staffDetails);

                return Ok(StaffId);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

