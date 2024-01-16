using FinalProjectCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public class MedicineRepository:IMedicineRepository
    {
        private readonly ASPCMSDBContext _Context;
        public MedicineRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }

        //Get all employees
        #region get all Medicine
        public async Task<List<TblMedicines>> GetAllTblMedicines()
        {
            if (_Context != null)
            {
                return await _Context.TblMedicines.ToListAsync();
            }
            return null;
        }
        #endregion

        #region Add a Medicine
        public async Task<int> AddMedicine(TblMedicines medicine)
        {
            if (_Context != null)
            {
                await _Context.TblMedicines.AddAsync(medicine);
                await _Context.SaveChangesAsync();  // commit the transction
                return (int)medicine.MedicineId;
            }
            return 0;
        }
        #endregion


        #region Update Medicine
        public async Task UpdateMedicine(TblMedicines medicine)
        {
            if (_Context != null)
            {
                _Context.Entry(medicine).State = EntityState.Modified;
                _Context.TblMedicines.Update(medicine);
                await _Context.SaveChangesAsync();
            }
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




        #region Delete Medicine
        public async Task<int> DeleteMedicine(int? id)
        {
            if (_Context != null)
            {
                var med = await (_Context.TblMedicines.FirstOrDefaultAsync(emp => emp.MedicineId == id));

                if (med != null)
                {
                    //Delete
                    _Context.TblMedicines.Remove(med);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return (int)med.MedicineId;
                }
            }
            return 0;
        }
        #endregion
    }
}
