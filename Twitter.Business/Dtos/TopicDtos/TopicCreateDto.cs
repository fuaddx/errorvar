using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Business.Dtos.TopicDtos
{
    public class TopicCreateDto
    {
        public string Name { get; set; }
    }
    public class TopicCreateDtoValidator : AbstractValidator<TopicCreateDto>
    {
        public TopicCreateDtoValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(32);
        }
    }
}
