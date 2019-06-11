using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rubi.Data.Models
{
    public class Clinic
    {
        [Required]
        public Guid ClinicId { get; set; }

        [Required]
        public string ClinicName { get; set; }

        [Required]
        public string Description { get; set; }

        public ClinicManager Manager { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();

        public ICollection<Staff> StaffList { get; set; } = new HashSet<Staff>();

        public ICollection<Patient> Patients { get; set; } = new HashSet<Patient>();

    }
}
