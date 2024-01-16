using FinalProjectCMS.ViewModel.Doctor;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public interface IPatientDetailsRepository
    {
        public Task<PatientDetails> GetPatientViewAsync(int appointmentId);

    }
}
