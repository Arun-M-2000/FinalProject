using FinalProjectCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public interface ILabRepository
    {
        Task<List<TblLabTests>> GetAllTblLabTests();
        Task<int> AddLabtest(TblLabTests lab);
        Task UpdateLabTest(TblLabTests lab);
        Task<TblLabTests> GetLabById(int? id);
        Task<int> DeleteLabTest(int? id);
    }
}
