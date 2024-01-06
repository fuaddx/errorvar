using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.AuthsDtos
{
    public class LoginDto
    {
        public string UserNameorEmail { get; set; }
        public string Password { get; set; }

    }
    public class LoginValidatorDto : AbstractValidator<LoginDto>
    {
        public LoginValidatorDto() {

            RuleFor(x => x.UserNameorEmail).NotEmpty()
                .NotNull()
                .WithMessage("Please enter our username or email.")
                .Must(value => IsValidEmail(value) || IsValidUserName(value));
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Please enter your Password")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters");

        }
        private bool IsValidEmail(string value)
        {
            return new EmailAddressAttribute().IsValid(value);
        }
        private bool IsValidUserName(string value)
        {
            return  !string.IsNullOrEmpty(value) && value.Length>=5 && value.Length<=20;
        }
    }
}
