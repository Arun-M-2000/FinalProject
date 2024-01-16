using FinalProjectCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FinalProjectCMS.ViewModel.LabTechnician;

namespace FinalProjectCMS.Repository.LabTechnician
{
    public class LabTestList : ILabTestList
    {
        private readonly ASPCMSDBContext _dbContext;

        public LabTestList(ASPCMSDBContext dbContext)
        {
            _dbContext= dbContext;
        }

            public async Task<List<LabTestVM>> GetViewModelPrescriptions()
            {
            if (_dbContext != null)
            {
                // LINQ
                // Assuming you have a DataContext named "dbContext"

                var detailsQuery = from lp in _dbContext.TblLabPrescriptions
                                   join a in _dbContext.Appointment on lp.AppointmentId equals a.AppointmentId
                                   join p in _dbContext.Patient on a.PatientId equals p.PatientId
                                   join d in _dbContext.TblDoctors on a.DocId equals d.DocId
                                   join s in _dbContext.TblStaffs on d.StaffId equals s.StaffId
                                   join l in _dbContext.TblLabTests on lp.LabTestId equals l.TestId
                                   select new LabTestVM
                                   {
                                       AppointmentId = a.AppointmentId,
                                       PatientName = p.PatientName,
                                       TestName = l.TestName,
                                       DoctorName = s.FullName, // Changed from "staff_Name" to "fullName"
                                       LabTestStatus = lp.LabTestStatus
                                   };


                return await detailsQuery.ToListAsync();
            }

            return null;

        }



    }
}
