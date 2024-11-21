using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Behaviours
{
    public  class KeywordSearchValidator:AbstractValidator<string>
    {
        public KeywordSearchValidator()
        {
            RuleFor(x => x)
               .NotEmpty()
               .NotNull()
               .MinimumLength(1)
               .MaximumLength(25);
        }
    }
}
