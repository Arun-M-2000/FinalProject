using FinalProjectCMS.ViewModel.Pharmacist;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public interface IPatientMedRepository
    {

        //ViewModel
        Task<IEnumerable<PatientMedViewModel>> GetViewModelPatientMed();
    }
}
