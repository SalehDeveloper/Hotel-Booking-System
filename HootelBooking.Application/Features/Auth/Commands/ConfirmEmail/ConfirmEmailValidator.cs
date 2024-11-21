using FluentValidation;

namespace HootelBooking.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailValidator :AbstractValidator<ConfirmEmailCommand>
    {

        public ConfirmEmailValidator()
        {
            RuleFor(x=> x.Request.Email).NotEmpty().NotNull();
            RuleFor(x=>x.Request.Code).NotEmpty().NotNull().MaximumLength(8).MinimumLength(8);   
        }

    }
}
