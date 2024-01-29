using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.LabTechnician;
using FinalProjectCMS.ViewModel.LabTechnician;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FinalProjectCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LlabTestsController : ControllerBase
    {
        private readonly ILabTestList _labTestRepository;
        private readonly ILabReportRepository _labReportRepository;


        // constructor injection

        public LlabTestsController(ILabTestList labTestRepository, ILabReportRepository labReportRepository)
        {
            _labTestRepository = labTestRepository;
            _labReportRepository = labReportRepository;
        }

        #region View Model
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestVM>>> LabTestsVM()
        {
            return await _labTestRepository.GetViewModelPrescriptions();
        }

        #endregion
        #region Listing
        [HttpGet]
        [Route("List")]

        public async Task<ActionResult<IEnumerable<LabReportVM>>> GetViewModelReport()
        {
            return await _labReportRepository.GetViewModelReport();
        }
        #endregion
        #region Add an report
        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody] LabReportVM report)
        {
            //check the validation of code
            if (ModelState.IsValid)
            {
                try
                {
                    var ReportId = await _labReportRepository.AddReport(report);
                    if (ReportId > 0)
                    {
                        return Ok(ReportId);
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
            return BadRequest(report);
        }
        #endregion
        #region
        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<GetIDVM>> GetIDViewModel(int AppointmentId)
        {
            var idvm = await _labReportRepository.GetIDViewModel(AppointmentId);
            return Ok(idvm);
        }
        #endregion
        #region
        [HttpGet]
        [Route("Bill")]

        public async Task<ActionResult<BillVM>> GetLabBillVm(int ReportId)
        {
            var idvm = await _labReportRepository.GetBillVM(ReportId);
            return Ok(idvm);
        }
        #endregion

    }
}

