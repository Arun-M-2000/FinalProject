using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Pharmacist;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public interface IPatientRepository
    {
        //ViewModel
        Task<IEnumerable<PatientViewModel>> GetViewModelPatient();


        //Find Patient - Get Patient by RegNo
        Task<PatientViewModel> GetPatientByRegNo(string? RegNo);
    }
}
