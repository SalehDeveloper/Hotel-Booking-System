using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Commands.AddState
{
    public class AddStateValidator:AbstractValidator<AddStateCommand>
    {
        public AddStateValidator()
        {
            RuleFor(x=> x.State.Name).NotEmpty().NotNull().WithMessage("Name Cannot Be Empty");
            RuleFor(x => x.State.IsActive).NotNull().NotEmpty();
            RuleFor(x => x.State.CountryId).NotNull().NotEmpty().GreaterThan(0);

        }
    }
}
