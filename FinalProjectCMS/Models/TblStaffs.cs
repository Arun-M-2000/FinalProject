using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblStaffs
    {
        public TblStaffs()
        {
            TblDoctors = new HashSet<TblDoctors>();
            TblReportGeneration = new HashSet<TblReportGeneration>();
        }

        public int StaffId { get; set; }
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public long MobileNo { get; set; }
        public int? LoginId { get; set; }
        public int? QualificationId { get; set; }
        public int? DepartmentId { get; set; }
        public string Email { get; set; }
        public int? SpecializationId { get; set; }
        public int? RoleId { get; set; }

        public virtual TblDepartments Department { get; set; }
        public virtual TblLoginUsers Login { get; set; }
        public virtual TblQualifications Qualification { get; set; }
        public virtual TblRoles Role { get; set; }
        public virtual TblSpecializations Specialization { get; set; }
        public virtual ICollection<TblDoctors> TblDoctors { get; set; }
        public virtual ICollection<TblReportGeneration> TblReportGeneration { get; set; }
    }
}
