namespace FinalProjectCMS.ViewModel.Pharmacist
{
    public class PatientMedViewModel
    {
        public string RegNo { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string PrescribedMedicine { get; set; }
        public string Dosage { get; set; }
        public int DosageDays { get; set; }
        public int MedicineQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string AvailableStock { get; set; }

    }
}
