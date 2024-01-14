using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblDepartments
    {
        public TblDepartments()
        {
            TblSpecializations = new HashSet<TblSpecializations>();
            TblStaffs = new HashSet<TblStaffs>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<TblSpecializations> TblSpecializations { get; set; }
        public virtual ICollection<TblStaffs> TblStaffs { get; set; }
    }
}
