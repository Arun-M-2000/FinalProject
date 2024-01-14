using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblDoctors
    {
        public TblDoctors()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DocId { get; set; }
        public int? StaffId { get; set; }
        public int? SpecializationId { get; set; }
        public decimal? ConsultationFee { get; set; }

        public virtual TblSpecializations Specialization { get; set; }
        public virtual TblStaffs Staff { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
