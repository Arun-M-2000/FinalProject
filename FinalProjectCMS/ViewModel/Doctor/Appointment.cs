namespace FinalProjectCMS.ViewModel.Doctor
{
    public class Appointment
    {

        public int appointment_Id { get; set; }

        public int Token_No { get; set; }
        public string Patient_Name { get; set; }
        public string Gender { get; set; }
        public int PatientAge { get; set; }
        public string CheckUp_Status { get; set; }

    }
}
