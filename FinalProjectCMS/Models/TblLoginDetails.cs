using System;
using System.Collections.Generic;

namespace FinalProjectCMS.Models
{
    public partial class TblLoginDetails
    {
        public int? StaffId { get; set; }
        public string StaffName { get; set; }
        public string RoleName { get; set; }
        public DateTime? LoginTime { get; set; }
    }
}
