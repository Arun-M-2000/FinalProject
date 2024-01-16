using FinalProjectCMS.Models;
using FinalProjectCMS.ViewModel.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Admin
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ASPCMSDBContext _Context;

        public StaffRepository(ASPCMSDBContext context)
        {
            _Context = context;
        }
        #region Update Staff
        public async Task UpdateStaff(TblStaffs staff)
        {
            if (_Context != null)
            {
                _Context.Entry(staff).State = EntityState.Modified;
                _Context.TblStaffs.Update(staff);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion



        #region Delete Staff
        public async Task<int> DeleteStaff(int? id)
        {
            if (_Context != null)
            {
                var staff = await (_Context.TblStaffs.FirstOrDefaultAsync(emp => emp.StaffId == id));

                if (staff != null)
                {
                    //Delete
                    _Context.TblStaffs.Remove(staff);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return staff.StaffId;
                }
            }
            return 0;
        }
        #endregion


        //---------------------List Staff-----------------------

        public async Task<List<StaffDetailsViewModel>> GetStaffDetails()
        {
            if (_Context != null)
            {
                var staffDetails = await (from staff in _Context.TblStaffs
                                          join department in _Context.TblDepartments on staff.DepartmentId equals department.DepartmentId into deptGroup
                                          from dept in deptGroup.DefaultIfEmpty()
                                          join specialization in _Context.TblSpecializations on dept.DepartmentId equals specialization.DepartmentId into specGroup
                                          from spec in specGroup.DefaultIfEmpty()
                                          join qualification in _Context.TblQualifications on staff.QualificationId equals qualification.QualificationId into qualGroup
                                          from qual in qualGroup.DefaultIfEmpty()
                                          join role in _Context.TblRoles on staff.RoleId equals role.RoleId into roleGroup
                                          from role in roleGroup.DefaultIfEmpty()
                                          join doctor in _Context.TblDoctors on staff.StaffId equals doctor.StaffId into doctorGroup
                                          from doctor in doctorGroup.DefaultIfEmpty()
                                          join login in _Context.TblLoginUsers on staff.LoginId equals login.LoginId into loginGroup
                                          from login in loginGroup.DefaultIfEmpty()

                                          select new StaffDetailsViewModel
                                          {
                                              StaffId = staff.StaffId,
                                              FullName = staff.FullName,
                                              Dob = staff.Dob,
                                              Gender = staff.Gender,
                                              Address = staff.Address,
                                              BloodGroup = staff.BloodGroup,
                                              JoiningDate = staff.JoiningDate,
                                              Salary = staff.Salary,
                                              MobileNo = staff.MobileNo,
                                              Email = staff.Email,
                                              DepartmentName = dept != null ? dept.DepartmentName : null,
                                              Qualification = qual != null ? qual.Qualification : null,
                                              Specialization = spec != null ? spec.Specialization : null,
                                              RoleName = role != null ? role.RoleName : null,
                                              ConsultationFee = doctor != null ? doctor.ConsultationFee : (decimal?)null,
                                              UserName = login.UserName,
                                              Password = login.Password,
                                              SpecializationId = spec != null ? spec.SpecializationId : (int?)null,
                                              RoleId = role != null ? role.RoleId : (int?)null,
                                              LoginId = login.LoginId,
                                              DepartmentId = dept != null ? dept.DepartmentId : (int?)null,
                                              QualificationId = (int)(qual != null ? qual.QualificationId : (int?)null)
                                          }).ToListAsync();

                return staffDetails;
            }

            // If _Context is null, return an empty list or handle it according to your requirements.
            return null;
        }






        public async Task<StaffDetailsViewModel> GetStaffDetailsById(int? staffId)
        {
            if (_Context != null)
            {
                var staffDetails = await (from staff in _Context.TblStaffs
                                          where staff.StaffId == staffId
                                          join department in _Context.TblDepartments on staff.DepartmentId equals department.DepartmentId into deptGroup
                                          from dept in deptGroup.DefaultIfEmpty()
                                          join specialization in _Context.TblSpecializations on dept.DepartmentId equals specialization.DepartmentId into specGroup
                                          from spec in specGroup.DefaultIfEmpty()
                                          join qualification in _Context.TblQualifications on staff.QualificationId equals qualification.QualificationId into qualGroup
                                          from qual in qualGroup.DefaultIfEmpty()
                                          join role in _Context.TblRoles on staff.RoleId equals role.RoleId into roleGroup
                                          from role in roleGroup.DefaultIfEmpty()
                                          join doctor in _Context.TblDoctors on staff.StaffId equals doctor.StaffId into doctorGroup
                                          from doctor in doctorGroup.DefaultIfEmpty()
                                          join login in _Context.TblLoginUsers on staff.LoginId equals login.LoginId into loginGroup
                                          from login in loginGroup.DefaultIfEmpty()
                                          select new StaffDetailsViewModel
                                          {
                                              StaffId = staff.StaffId,
                                              FullName = staff.FullName,
                                              Dob = staff.Dob,
                                              Gender = staff.Gender,
                                              Address = staff.Address,
                                              BloodGroup = staff.BloodGroup,
                                              JoiningDate = staff.JoiningDate,
                                              Salary = staff.Salary,
                                              MobileNo = staff.MobileNo,
                                              Email = staff.Email,
                                              DepartmentName = dept != null ? dept.DepartmentName : null,
                                              Qualification = qual != null ? qual.Qualification : null,
                                              Specialization = spec != null ? spec.Specialization : null,
                                              RoleName = role != null ? role.RoleName : null,
                                              ConsultationFee = doctor != null ? doctor.ConsultationFee : (decimal?)null,
                                              UserName = login.UserName,
                                              Password = login.Password
                                          }).FirstOrDefaultAsync();

                return staffDetails;
            }

            // If _Context is null, return null or handle it according to your requirements.
            return null;
        }



        //--------------------------Adding the Staff-------------------------------

        public async Task<TblDepartments> GetDepartmentId(int departmentId)
        {
            if (_Context != null)
            {

                return await _Context.TblDepartments
                    .Where(q => q.DepartmentId == departmentId)
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<TblSpecializations>> GetSpecializationsForDepartmentAsync(int departmentId)
        {
            if (_Context != null)
            {
                return await _Context.TblSpecializations
                    .Where(s => s.DepartmentId == departmentId)
                    .ToListAsync();
            }

            return null;
        }



        public async Task UpdateID(int staffId, int specializationId, int departmentId)
        {
            if (_Context != null)
            {
                var staff = await _Context.TblStaffs.FindAsync(staffId);

                if (staff != null)
                {
                    // Update the properties
                    staff.SpecializationId = specializationId;
                    staff.DepartmentId = departmentId;

                    // Mark the entity as modified
                    _Context.Entry(staff).State = EntityState.Modified;

                    // Save changes
                    await _Context.SaveChangesAsync();
                }
            }
        }






        public async Task<int> AddStaffWithRelatedData(StaffDetailsViewModel staffDetails)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();




            try
            {
                var nelogin = new TblLoginUsers
                {
                    UserName = staffDetails.UserName,
                    Password = staffDetails.Password,
                    RoleId = staffDetails.RoleId
                };
                _Context.TblLoginUsers.Add(nelogin);
                await _Context.SaveChangesAsync();
                // Add staff to the staff table
                var newStaff = new TblStaffs
                {
                    FullName = staffDetails.FullName,
                    Dob = staffDetails.Dob,
                    Gender = staffDetails.Gender,
                    Address = staffDetails.Address,
                    BloodGroup = staffDetails.BloodGroup,
                    JoiningDate = staffDetails.JoiningDate,
                    Salary = staffDetails.Salary,
                    MobileNo = staffDetails.MobileNo,
                    Email = staffDetails.Email,
                    RoleId = staffDetails.RoleId,
                    SpecializationId = staffDetails.SpecializationId,
                    DepartmentId = staffDetails.DepartmentId,
                    QualificationId = staffDetails.QualificationId,
                    LoginId = nelogin.LoginId


                };



                _Context.TblStaffs.Add(newStaff);
                await _Context.SaveChangesAsync();


                int newStaffId = newStaff.StaffId;




                if (staffDetails.RoleId == 3)
                {
                    var doctor = new TblDoctors
                    {
                        StaffId = newStaffId,
                        ConsultationFee = staffDetails.ConsultationFee,
                        SpecializationId = staffDetails.SpecializationId
                    };

                    _Context.TblDoctors.Add(doctor);
                    await _Context.SaveChangesAsync();
                }







                await transaction.CommitAsync();

                // Return the ID of the newly added staff
                return newStaffId;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                // Log the exception or handle it as needed
                throw;
            }



        }



    }
}
