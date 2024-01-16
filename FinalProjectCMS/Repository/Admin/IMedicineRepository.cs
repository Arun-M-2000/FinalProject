using FinalProjectCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public interface IMedicineRepository
    {
        Task<List<TblMedicines>> GetAllTblMedicines();
        Task<int> AddMedicine(TblMedicines medicine);
        Task UpdateMedicine(TblMedicines medicine);
        Task<TblMedicines> GetMedicineById(long? id);
        Task<int> DeleteMedicine(int? id);
    }
}
