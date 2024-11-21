using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Commands.UpdateState
{
    public class UpdateStateValidator:AbstractValidator<UpdateStateCommand>
    {
        public UpdateStateValidator()
        {
            RuleFor(x => x.State.Name).NotEmpty().NotNull();
            RuleFor(x => x.State.IsActive).NotEmpty().NotNull();
            RuleFor(x => x.State.Id).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.State.CountryId).NotEmpty().NotNull().GreaterThan(0);

        }
    }
}
