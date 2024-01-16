using FinalProjectCMS.ViewModel.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public interface IPatientHistoryRepository
    {

        public  Task<List<PatientHistory>> GetPatientHistoryAsync(int patientId);
        
     }
}
