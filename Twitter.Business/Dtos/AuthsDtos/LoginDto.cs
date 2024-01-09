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
                .MinimumLength(3)
                .MaximumLength(64);
            RuleFor(x => x.Password).NotEmpty()
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(64);
        }
    }
}
