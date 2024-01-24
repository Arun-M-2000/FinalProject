using System;

namespace FinalProjectCMS.ViewModel.Pharmacist
{
    public class PatientViewModel
    {
        public string RegNo { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public long PhNo { get; set; }
        public string DoctorName { get; set; }
        //public string Specialization { get; set; }
    }
}
