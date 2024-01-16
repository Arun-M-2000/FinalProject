using System;

namespace FinalProjectCMS.ViewModel.Doctor
{
    public class AppointmentsVM
    {

        public int Appointment_Id { get; set; }
        public int Token_No { get; set; }
        public string Patient_Name { get; set; }
        public string Gender { get; set; }
        public int PatientAge { get; set; }
        public string CheckUp_Status { get; set; }
        public DateTime Appointment_Date { get; set; }
        public int Doc_Id { get; set; }

    }
}
