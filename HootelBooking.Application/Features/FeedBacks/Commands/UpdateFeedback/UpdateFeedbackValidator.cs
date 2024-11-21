using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback
{
    public class UpdateFeedbackValidator:AbstractValidator<UpdateFeedbackCommand>
    {
        public UpdateFeedbackValidator()
        {
            RuleFor(x=> x.Request.Rate).NotEmpty().NotNull().InclusiveBetween(0, 5);
        }
    }
}
