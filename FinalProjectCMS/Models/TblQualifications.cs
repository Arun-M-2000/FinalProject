using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblQualifications
    {
        public TblQualifications()
        {
            TblStaffs = new HashSet<TblStaffs>();
        }

        public int QualificationId { get; set; }
        public string Qualification { get; set; }

        public virtual ICollection<TblStaffs> TblStaffs { get; set; }
    }
}
