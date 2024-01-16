using FinalProjectCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public interface IMedicineRepository
    {

        // only declarations -- Abstraction
        //all the data should be accepted on asynchronous manner
        //Get all the Employees  -- Select  --Retrive
        Task<List<TblMedicines>> GetAllTblMedicines();//



        //Find an Employee - Get Medicine by id

        Task<TblMedicines> GetMedicineById(long? id);
    }
}
