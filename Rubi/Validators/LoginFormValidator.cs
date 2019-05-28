using FluentValidation;
using Rubi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Validators
{
    public class LoginFormValidator : AbstractValidator<LoginFormDto>
    {
        public LoginFormValidator()
        {
            RuleFor(u => u.Email)
               .NotNull()
               .NotEmpty()
               .Matches(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
               .WithMessage($"The field Email cannot be empty or null and must be valid pattern.");
           
            //TODO with inject service for passing unique emails
            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage($"The field Password cannot be empty or null. The length of field must be minimum six symbols");
        }
    }
}
