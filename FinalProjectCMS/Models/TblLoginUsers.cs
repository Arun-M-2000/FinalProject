using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblLoginUsers
    {
        public TblLoginUsers()
        {
            TblStaffs = new HashSet<TblStaffs>();
        }

        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public virtual TblRoles Role { get; set; }
        public virtual ICollection<TblStaffs> TblStaffs { get; set; }
    }
}
