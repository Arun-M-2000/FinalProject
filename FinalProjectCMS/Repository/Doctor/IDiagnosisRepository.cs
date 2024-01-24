using FinalProjectCMS.ViewModel.Doctor;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public interface IDiagnosisRepository
    {
        public  Task<int?> FillDiagForm(DiagnosisVM diagnosisVM);

    }
}
