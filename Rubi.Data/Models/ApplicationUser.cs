﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Rubi.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubi.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The First Name should contain only letters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(DataConstants.NAME_MIN_LENGTH)]
        [MaxLength(DataConstants.NAME_MAX_LENGTH)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Last Name should contain only letters.")]
        public string LastName { get; set; }

        //public byte[] ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        [MinLength(DataConstants.MIX_BIRTHDATE_YEAR)]
        [MaxLength(DataConstants.MAX_BIRTHDATE_YEAR)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Address { get; set; }

        //[ForeignKey("Doctor")]
        public Doctor Doctor { get; set; }

        //[ForeignKey("Patient")]
        public Patient Patient { get; set; }
    }
}
