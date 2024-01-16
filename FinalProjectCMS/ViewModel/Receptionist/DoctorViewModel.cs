using Microsoft.VisualBasic;
using System;

namespace FinalProjectCMS.ViewModel.Receptionist
{
    public class DoctorViewModel
    {
        public int DocId {  get; set; }
        public decimal? ConsultationFee {  get; set; }
        public int? StaffId { get; set; }

        public int? SpecializationId { get; set; }
        public int? QualificationId { get; set; }

        public int? LoginId {  get; set; }

        public string FullName {  get; set; }
        public DateTime? Dob {  get; set; }
        public string Gender { get; set; }
       
        public string BloodGroup {  get; set; }
        public DateTime? JoiningDate { get; set; }

        public decimal? Salary { get; set; }   
        public long? MobileNo { get; set; } 
        public int? DepartmentId { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }



    }
}
