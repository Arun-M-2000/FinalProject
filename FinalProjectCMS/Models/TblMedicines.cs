using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblMedicines
    {
        public TblMedicines()
        {
            TblMedicinePrescriptions = new HashSet<TblMedicinePrescriptions>();
        }

        public long MedicineId { get; set; }
        public string MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string CompanyName { get; set; }
        public string StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual ICollection<TblMedicinePrescriptions> TblMedicinePrescriptions { get; set; }
    }
}
