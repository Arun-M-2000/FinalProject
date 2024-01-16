namespace FinalProjectCMS.ViewModel.Pharmacist
{
    public class PatientMedViewModel
    {
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string PrescribedMedicine { get; set; }
        public int DosageDays { get; set; }
        public int DosageQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string AvailableStock { get; set; }

    }
}
