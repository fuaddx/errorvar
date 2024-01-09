using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.AuthsDtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() {
            RuleFor(x => x.Name).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(32);
            RuleFor(x => x.Surname).NotEmpty()
                .MinimumLength(3)
                .MaximumLength(35);
            RuleFor(x => x.Username).NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(35);
            RuleFor(x => x.Password).NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}")
                .WithMessage("Password must contain at least 8 characters");
            RuleFor(x => x.Email).NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email address must be a Gmail address");
            RuleFor(x => x.BirthDay).NotEmpty()
                .Must(birthday => birthday <= DateTime.UtcNow)
                .WithMessage("Birt date cannot be in the future");
        }
    }
}
