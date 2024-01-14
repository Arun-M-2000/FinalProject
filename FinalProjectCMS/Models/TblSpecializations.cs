using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblSpecializations
    {
        public TblSpecializations()
        {
            TblDoctors = new HashSet<TblDoctors>();
            TblStaffs = new HashSet<TblStaffs>();
        }

        public int SpecializationId { get; set; }
        public string Specialization { get; set; }
        public int? DepartmentId { get; set; }

        public virtual TblDepartments Department { get; set; }
        public virtual ICollection<TblDoctors> TblDoctors { get; set; }
        public virtual ICollection<TblStaffs> TblStaffs { get; set; }
    }
}
