using FluentValidation;
using Rubi.Constants;
using Rubi.Dtos;
using Rubi.Services.EmailChecker.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Validators
{
    public class RegisterFormValidator : AbstractValidator<RegisterFormDto>
    {
        private readonly IEmailChecker emailCheckerService;

        public RegisterFormValidator(IEmailChecker emailCheckerService)
        {
            this.emailCheckerService = emailCheckerService;

            RuleFor(u => u.Username)
                .NotNull()
                .NotEmpty()
                .Length(DataConstants.USERNAME_MIN_LENGTH, DataConstants.USERNAME_MAX_LENGTH)
                .WithMessage($"The field Username cannot be empty or null. The length of field must be between {DataConstants.USERNAME_MIN_LENGTH} and {DataConstants.USERNAME_MAX_LENGTH}");

            RuleFor(u => u.Email)
               .NotNull()
               .NotEmpty()
               .Must(ValidateMailIfExist)
               .WithMessage("Current email is exist in db")
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
               .Length(DataConstants.NAME_MIN_LENGTH, DataConstants.NAME_MAX_LENGTH)
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
               .GreaterThanOrEqualTo(a => DateTime.Parse("1/1/1900"))
               .LessThanOrEqualTo(a => DateTime.Parse("1/1/2010"))
               .WithMessage($"The field Birthdate cannot be empty or null.");

            RuleFor(u => u.Address)
               .NotNull()
               .NotEmpty()
               .WithMessage("Address is required");
        }

        private bool ValidateMailIfExist(string email)
        {
            if (this.emailCheckerService.EmailExist(email))
            {
                return false;
            }

            return true;      
        }
    }
}