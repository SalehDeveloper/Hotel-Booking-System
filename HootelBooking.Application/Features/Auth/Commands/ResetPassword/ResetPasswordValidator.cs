using FluentValidation;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordCommand>
    {
      
        public ResetPasswordValidator()
        {    
         

            RuleFor(x => x.Request.Email).NotEmpty().NotNull().EmailAddress();
           
            RuleFor(x => x.Request.NewPassword).NotNull().NotEmpty().MinimumLength(8).Matches("[A-Z]").
             WithMessage("Password must contain at least one uppercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one special character");
            
            RuleFor(x=> x.Request.Code).NotEmpty().NotNull().MaximumLength(8).MinimumLength(8); 


         
        }


       
    }
}
