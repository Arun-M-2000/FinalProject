using FinalProjectCMS.ViewModel.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public interface IAppointmentRepository
    {
        public  Task<List<AppointmentsVM>> GetAppointmentViewAsync(int docId);


    }
}
