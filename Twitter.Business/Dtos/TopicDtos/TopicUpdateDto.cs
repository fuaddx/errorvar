using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.TopicDtos
{
    public class TopicUpdateDto
    {
        public string Name { get; set; }
    }
    public class TopicUpdateDtoValidator : AbstractValidator<TopicUpdateDto>
    {
        public TopicUpdateDtoValidator() { 
            RuleFor(X=>X.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(32);
        }
    }
}
