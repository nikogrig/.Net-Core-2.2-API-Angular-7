using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Data.Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }

        public string Description { get; set; }

        public Guid ClinicId { get; set; }

        public Clinic Clinic { get; set; }

    }
}
