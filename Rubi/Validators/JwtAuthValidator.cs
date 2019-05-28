using FluentValidation;
using Rubi.Constants;
using Rubi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Validators
{
    public class JwtAuthValidator : AbstractValidator<JwtAuthDto>
    {
        public JwtAuthValidator()
        {
            RuleFor(u => u.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage($"The field ID cannot be empty or null.");

            RuleFor(u => u.Username)
                .NotNull()
                .NotEmpty()
                .Length(DataConstants.USERNAME_MIN_LENGTH, DataConstants.USERNAME_MAX_LENGTH)
                .WithMessage($"The field Username cannot be empty or null. The length of field must be between {DataConstants.USERNAME_MIN_LENGTH} and {DataConstants.USERNAME_MAX_LENGTH}");

            RuleFor(u => u.Email)
               .NotNull()
               .NotEmpty()
               .Matches(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
               .WithMessage($"The field Email cannot be empty or null and must be valid pattern.");

            RuleFor(u => u.FirstName)
               .NotNull()
               .NotEmpty()
               .Length(DataConstants.NAME_MIN_LENGTH, DataConstants.NAME_MAX_LENGTH)
               .Matches(@"^[a-zA-Z]+$")
               .WithMessage($"The field FirstName cannot be empty or null. The length of field must be between {DataConstants.NAME_MIN_LENGTH} and {DataConstants.NAME_MAX_LENGTH} and should contain only letters.");

            RuleFor(u => u.LastName)
               .NotNull()
               .NotEmpty()
               .Length(DataConstants.NAME_MAX_LENGTH, DataConstants.NAME_MAX_LENGTH)
               .Matches(@"^[a-zA-Z]+$")
               .WithMessage($"The field LastName cannot be empty or null. The length of field must be between {DataConstants.NAME_MIN_LENGTH} and {DataConstants.NAME_MAX_LENGTH} and should contain only letters.");

            RuleFor(u => u.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Matches(@"^[+0-9]{10,14}$")
                .WithMessage("Phone number must contain only digits and start can be start with '+' sign");

            RuleFor(u => u.Birthdate)
               .NotEmpty()
               .NotNull()
               .LessThanOrEqualTo(a => DateTime.Parse("1/1/1900"))
               .GreaterThanOrEqualTo(a => DateTime.Parse("1/1/2010"))
               .WithMessage($"The field Birthdate cannot be empty or null. The length of field must be between {DataConstants.NAME_MIN_LENGTH} and {DataConstants.NAME_MAX_LENGTH} and should contain only letters.");

            RuleFor(u => u.Address)
               .NotNull()
               .NotEmpty()
               .WithMessage("Address is required");
        }
    }
}
