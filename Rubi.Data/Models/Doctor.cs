using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rubi.Data.Models
{
    public class Doctor
    {
        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Guid? UserId { get; set; }

        public ApplicationUser AppClient { get; set; }

        public Guid ClinicId { get; set; }

        public Clinic Clinic { get; set; }
    }
}
