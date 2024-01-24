using FinalProjectCMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Receptionist
{
    public interface IPatientRepository
    {
       Task<List<Patient>> GetAllPatient();

        Task<int> AddPatient(Patient pa);
        Task<Patient> UpdatePatient(Patient pa);
        Task<Patient> GetPatientById(int? id);
        Task<Patient> DisableStatus(int? paitientId);

        Task<List<Patient>> GetAllDisabledPatients();
        Task<Patient> EnableStatus(int? paitientId);



    }
}
