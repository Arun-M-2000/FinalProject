
﻿using FinalProjectCMS.ViewModel.Doctor;

﻿using FinalProjectCMS.Models;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProjectCMS.Repository.Doctor
{
    public interface IAppointmentRepository
    {

        public  Task<List<AppointmentsVM>> GetAppointmentViewAsync(int docId);



    }
}
