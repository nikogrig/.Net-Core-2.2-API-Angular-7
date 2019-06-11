using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rubi.Data.Models
{
    public class ClinicManager
    {
        [Required]
        [ForeignKey("Clinic")]
        public Guid ClinicManagerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Guid ClinicId { get; set; }

        public Clinic Clinic { get; set; }
    }
}
