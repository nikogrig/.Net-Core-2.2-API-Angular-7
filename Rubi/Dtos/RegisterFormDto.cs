using System;
using System.ComponentModel.DataAnnotations;
namespace Rubi.Dtos
{
    public class RegisterFormDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        //[MinLength(DataConstants.NAME_MIN_LENGTH)]
        //[MaxLength(DataConstants.NAME_MAX_LENGTH)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The First Name should contain only letters.")]
        public string FirstName { get; set; }

        [Required]
        //[MinLength(DataConstants.NAME_MIN_LENGTH)]
        //[MaxLength(DataConstants.NAME_MAX_LENGTH)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Last Name should contain only letters.")]
        public string LastName { get; set; }

        //[DataType(DataType.Date)]
        //[MinLength(DataConstants.MIX_BIRTHDATE_YEAR)]
        //[MaxLength(DataConstants.MAX_BIRTHDATE_YEAR)]
        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        //public ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
