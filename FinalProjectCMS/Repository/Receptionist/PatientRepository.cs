using FinalProjectCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinalProjectCMS.Repository.Receptionist
{
    public class PatientRepository : IPatientRepository
    {
        
        //Data fields
        private readonly ASPCMSDBContext _dbContext;

        public PatientRepository(ASPCMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region
        //Listing All Patients 


        public async Task<List<Patient>> GetAllPatient()
        {
            if (_dbContext != null)
            {
                return await _dbContext.Patient.Where(p=>p.PatientStatus == "ACTIVE").ToListAsync();
            }
            return null;
        }

        #endregion


        #region Add a Patient

        public async Task<int> AddPatient(Patient pa)
        {
            if (_dbContext != null)
            {
                await _dbContext.Patient.AddAsync(pa);
                // for commiting the transaction
                await _dbContext.SaveChangesAsync();

                return pa.PatientId;
            }

            return 0;
        }

        #endregion

        #region Updating the Patient

        public async Task<Patient> UpdatePatient(Patient pa)
        {
            if (_dbContext != null)
            {
                _dbContext.Entry(pa).State = EntityState.Modified; // to modifying the values
                _dbContext.Patient.Update(pa);
                await _dbContext.SaveChangesAsync();
            }
            return null;
        }

        #endregion

        // get Patient by name and phone number



       
        #region GetPatientById
        public async Task<Patient> GetPatientById(int? id)
        {
            if (_dbContext != null)
            {
                var medicine = await _dbContext.Patient.FindAsync(id);   //primary key
                return medicine;
            }
            return null;
        }
        #endregion

        
        //Disable patient 

        #region Disable Patient Status
        public async Task<Patient> DisableStatus(int? paitientId)
        {
            var patient = await _dbContext.Patient.FindAsync(paitientId);
            if (patient != null)
            {
                patient.PatientStatus = "Inactive";
                await _dbContext.SaveChangesAsync();
            }
            return patient;
        }
        #endregion


       #region Get All Disabled Patient Records
        public async Task<List<Patient>> GetAllDisabledPatients()
        {
            if (_dbContext != null)
            {
                return await _dbContext.Patient.Where(p => p.PatientStatus == "Inactive").ToListAsync();
            }
            return null;
        }
        #endregion

        #region Enable Patient Status
        public async Task<Patient> EnableStatus(int? paitientId)
        {
            var patient = await _dbContext.Patient.FindAsync(paitientId);
            if (patient != null)
            {
                patient.PatientStatus = "ACTIVE";
                await _dbContext.SaveChangesAsync();
            }
            return patient;
        }
        #endregion
    }


}
