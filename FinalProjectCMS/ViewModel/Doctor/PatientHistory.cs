using System;

namespace FinalProjectCMS.ViewModel.Doctor
{
    public class PatientHistory
    {
        public int Appointment_Id { get; set; }
        public DateTime appointment_Date { get; set; }
        public string symptoms { get; set; }
        public string diagnosis_Desc { get; set; }
        public string medpres_Name { get; set; }
        public string labtest_Name { get; set; }
        public string lab_Result { get; set; }
        public string doc_notes { get; set; }


    }
}
