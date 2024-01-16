using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Receptionist;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Receptionist
{
    public interface IAppointmentsRepository
    {
        Task<List<TblDepartments>> GetAllDepartment();

       
        Task<List<TblSpecializations>> GetAllSpecializationByDepartmentId(int? departmentId);
        Task<List<DoctorViewModel>> GetAllDoctorBySpecializationId(int? specializationId);
        Task<Appointment_ViewModel> BookAppointment(Appointment_ViewModel viewModel, bool isNewPatient);
        Task<BillViewModel> BillDetails(int? billId);
        Task<List<BillViewModel>> GetAllAppointmentsWithBillViewModel();
        Task<BillViewModel> GetAppointmentDetailsById(int? appointmentId);
        Task<Appointment> CancelAppointment(int? appointmentId);

       

    }
}
