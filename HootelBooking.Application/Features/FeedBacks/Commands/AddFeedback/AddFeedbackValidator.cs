using FluentValidation;
using HootelBooking.Application.Features.FeedBacks.Commands.UpdateFeedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.FeedBacks.Commands.AddFeedback
{
    

    public class AddFeedbackValidator : AbstractValidator<AddFeedbackCommand>
    {
        public AddFeedbackValidator()
        {
            RuleFor(x => x.Request.Rate).NotEmpty().NotNull().InclusiveBetween(0, 5);
        }
    }
}
