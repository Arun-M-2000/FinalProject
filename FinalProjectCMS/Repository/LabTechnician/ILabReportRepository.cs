using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.LabTechnician;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.LabTechnician
{
    public interface ILabReportRepository
    {
        Task<List<LabReportVM>> GetViewModelReport();
        Task<GetIDVM> GetIDViewModel(int AppointmentId);
        Task<int> AddReport(LabReportVM viewmodal);
        Task<BillVM> GetBillVM(int ReportId);



    }
}
