using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblRoles
    {
        public TblRoles()
        {
            TblLoginUsers = new HashSet<TblLoginUsers>();
            TblStaffs = new HashSet<TblStaffs>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<TblLoginUsers> TblLoginUsers { get; set; }
        public virtual ICollection<TblStaffs> TblStaffs { get; set; }
    }
}
