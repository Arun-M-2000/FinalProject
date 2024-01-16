using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public interface IStaffRepository
    {
        Task UpdateStaff(TblStaffs staff);

        Task<int> DeleteStaff(int? id);
        Task<List<StaffDetailsViewModel>> GetStaffDetails();
        Task<StaffDetailsViewModel> GetStaffDetailsById(int? staffId);

        Task<int> AddStaffWithRelatedData(StaffDetailsViewModel staffDetails);

    }
}
