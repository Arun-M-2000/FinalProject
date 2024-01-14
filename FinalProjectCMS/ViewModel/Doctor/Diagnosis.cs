namespace FinalProjectCMS.ViewModel.Doctor
{
    public class Diagnosis
    {
        public int Appointment_Id { get; set; }
        public string Symptoms { get; set; }
        public string Diag_Descrip { get; set; }
        public string Doct_Notes { get; set; }
        public string Medpres_Name { get; set; }
        public string Medpres_Dosage { get; set; }
        public int Medpres_Dosagedays { get; set; }
        public int Medpres_Quantity { get; set; }
        public string LabTest_Name { get; set; }
        public string Lab_Note { get; set; }



    }
}
