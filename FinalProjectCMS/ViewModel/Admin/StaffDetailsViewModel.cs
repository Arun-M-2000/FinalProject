using System;

namespace FinalProjectCMS.ViewModel.Admin
{
    public class StaffDetailsViewModel
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public long MobileNo { get; set; }
        public string Email { get; set; }
        public string? DepartmentName { get; set; }
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public string? RoleName { get; set; }
        public int? RoleId { get; set; }
        public int LoginId { get; set; }
        public int? SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        public int QualificationId { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
