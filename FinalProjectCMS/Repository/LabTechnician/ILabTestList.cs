using FinalProjectCMS.ViewModel.LabTechnician;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.LabTechnician
{
    public interface ILabTestList
    {
        Task<List<LabTestVM>> GetViewModelPrescriptions();

    }
}
