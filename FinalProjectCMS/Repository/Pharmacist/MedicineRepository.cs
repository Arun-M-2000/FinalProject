using FinalProjectCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Pharmacist
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ASPCMSDBContext _Context;

        public MedicineRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }

        //Get all Medicines
        #region get all Medicines
        public async Task<List<TblMedicines>> GetAllTblMedicines()
        {
            if (_Context != null)
            {
                return await _Context.TblMedicines.ToListAsync();
            }
            return null;
        }
        #endregion

        #region GetMedicineById
        public async Task<TblMedicines> GetMedicineById(long? id)
        {
            if (_Context != null)
            {
                var medicine = await _Context.TblMedicines.FindAsync(id);   //primary key
                return medicine;
            }
            return null;
        }
        #endregion

    }


}
