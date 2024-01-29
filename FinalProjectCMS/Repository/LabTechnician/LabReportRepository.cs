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
        #region
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
        #endregion
        #region GET
        public async Task<GetIDVM> GetIDViewModel(int AppointmentId)
        {
            if (_dbContext != null)
            {
                var query = from lr in _dbContext.TblLabPrescriptions
                            join a in _dbContext.Appointment on lr.AppointmentId equals a.AppointmentId
                            join p in _dbContext.TblDoctors on a.DocId equals p.DocId
                            join s in _dbContext.TblStaffs on p.StaffId equals s.StaffId
                            where lr.AppointmentId == AppointmentId
                            select new GetIDVM
                            {
                                LabPrescriptionId = lr.LabPrescriptionId,
                                AppointmentId = lr.AppointmentId,
                                TestId = lr.LabTestId,
                                StaffId = s.StaffId,

                            };

                return await query.FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion

        #region POST
        public async Task<int> AddReport(LabReportVM viewmodal)
        {
            if (_dbContext != null)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Creating a new LabReportGeneration object and adding it to the context
                        TblReportGeneration report = new TblReportGeneration()
                        {
                            AppointmentId = viewmodal.AppointmentId,
                            TestResult = viewmodal.TestResult,
                            ReportDate = viewmodal.ReportDate,
                            Remarks = viewmodal.Remarks,
                            TestId = viewmodal.TestId,
                            StaffId = viewmodal.StaffId
                        };

                        _dbContext.TblReportGeneration.Add(report);
                        await _dbContext.SaveChangesAsync();

                        // Assuming that LabReportGeneration has an AppointmentId field
                        int newReportId = report.ReportId;

                        // Create a LabBillGeneration entry
                        TblBillGeneration labBill = new TblBillGeneration()
                        {
                            ReportId = newReportId,
                            BillDate = DateTime.Now, // You may customize this as needed
                            TotalAmount = CalculateTotalAmount(viewmodal.TestId) // Replace with your logic for calculating total amount
                        };

                        _dbContext.TblBillGeneration.Add(labBill);
                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();

                        return newReportId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
            return 0;
        }


        #endregion

        #region Bill Generation
        public async Task<BillVM> GetBillVM(int ReportId)
        {
            if (_dbContext != null)
            {
                var query = from lb in _dbContext.TblBillGeneration
                            join l in _dbContext.TblReportGeneration on lb.ReportId equals l.ReportId
                            join a in _dbContext.Appointment on l.AppointmentId equals a.AppointmentId
                            join p in _dbContext.Patient on a.PatientId equals p.PatientId
                            join t in _dbContext.TblLabTests on l.TestId equals t.TestId
                            where lb.ReportId == ReportId
                            select new BillVM
                            {
                                BillId = lb.BillId,
                                AppointmentId = l.AppointmentId, // Assuming LabReportGeneration has an AppointmentId
                                TestId = l.TestId,
                                TestName = t.TestName,
                                Price = t.Price,
                                TotalAmount = lb.TotalAmount + lb.TotalAmount * 0.18m,
                                PatientId = p.PatientId,
                                PatientName = p.PatientName,
                                ReportId = lb.ReportId,
                            };

                return await query.FirstOrDefaultAsync();
            }
            return null;
        }



        #endregion

        private decimal CalculateTotalAmount(int? testId)
        {
            if (testId.HasValue)
            {
                // Retrieve the test price based on the provided testId
                TblLabTests labTest = _dbContext.TblLabTests.FirstOrDefault(t => t.TestId == testId);

                if (labTest != null)
                {
                    // Replace this logic with your actual pricing calculation
                    decimal testPrice = labTest.Price;

                    // Calculate total amount (including GST)
                    decimal totalAmount = testPrice + (testPrice * 0.18m); // Assuming 18% GST

                    return totalAmount;
                }
            }

            return 0.00m;
        }


    }
}
