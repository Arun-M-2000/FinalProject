using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.LabTechnician;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.LabTechnician
{
    public class LabReportRepository : ILabReportRepository
    {

        private readonly ASPCMSDBContext _dbContext;

        public LabReportRepository(ASPCMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabReportVM>> GetViewModelReport()
        {
            if (_dbContext != null)
            {
                var query = from lr in _dbContext.TblReportGeneration
                            join l in _dbContext.TblLabTests on lr.TestId equals l.TestId
                            join a in _dbContext.Appointment on lr.AppointmentId equals a.AppointmentId
                            join p in _dbContext.Patient on a.PatientId equals p.PatientId

                            select new LabReportVM
                            {
                                ReportDate = lr.ReportDate,
                                ReportId = lr.ReportId,
                                PatientName =p. PatientName,
                                TestName = l.TestName,
                                HighRange = l.HighRange,
                                LowRange = l.LowRange,
                                TestResult = lr.TestResult,
                                Remarks = lr.Remarks
                            };

                return await query.ToListAsync();
            }
            return null;
        }
        #region 
        public async Task<int> AddReport(TblReportGeneration report)
        {
            if (_dbContext != null)
            {
                await _dbContext.TblReportGeneration.AddAsync(report);
                await _dbContext.SaveChangesAsync();
                return report.ReportId;
            }
            return 0;
        }
        #endregion

        #region GET
        public async Task<GetIDVM> GetIDViewModel()
        {
            if (_dbContext != null)
            {
                var query = from lr in _dbContext.TblLabPrescriptions
                            join a in _dbContext.Appointment on lr.AppointmentId equals a.AppointmentId
                            join p in _dbContext.TblDoctors on a.DocId equals p.DocId
                            join s in _dbContext.TblStaffs on p.StaffId equals s.StaffId
                            select new GetIDVM
                            {
                                AppointmentId = lr.AppointmentId,
                                TestId = lr.LabTestId,
                                StaffId = s.StaffId,
                                
                            };

                return await query.FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion
    }
}
