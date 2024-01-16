using FinalProjectCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public class LabRepository:ILabRepository
    {
        private readonly ASPCMSDBContext _Context;
        public LabRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }
        #region get all Labtests
        public async Task<List<TblLabTests>> GetAllTblLabTests()
        {
            if (_Context != null)
            {
                return await _Context.TblLabTests.ToListAsync();
            }
            return null;
        }
        #endregion

        #region Add a Labtests
        public async Task<int> AddLabtest(TblLabTests lab)
        {
            if (_Context != null)
            {
                await _Context.TblLabTests.AddAsync(lab);
                await _Context.SaveChangesAsync();  // commit the transction
                return lab.TestId;
            }
            return 0;
        }
        #endregion


        #region Update Lab
        public async Task UpdateLabTest(TblLabTests lab)
        {
            if (_Context != null)
            {
                _Context.Entry(lab).State = EntityState.Modified;
                _Context.TblLabTests.Update(lab);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetLabById
        public async Task<TblLabTests> GetLabById(int? id)
        {
            if (_Context != null)
            {
                var employee = await _Context.TblLabTests.FindAsync(id);   //primary key
                return employee;
            }
            return null;
        }
        #endregion



        #region Delete Lab
        public async Task<int> DeleteLabTest(int? id)
        {
            if (_Context != null)
            {
                var lab = await (_Context.TblLabTests.FirstOrDefaultAsync(emp => emp.TestId == id));

                if (lab != null)
                {
                    //Delete
                    _Context.TblLabTests.Remove(lab);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return lab.TestId;
                }
            }
            return 0;
        }
        #endregion
    }
}
