using FluentValidation;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Request;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator:AbstractValidator<RegisterCommand>
    {
     
        public RegisterValidator(UserManager<ApplicationUser> userManager  , ICountryRepository countryRepository)
        {  

            RuleFor(x => x.Request.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull()
                .WithMessage("Please Enter A Valid Email Address");
                
           
            RuleFor(x => x.Request.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Name Cannot Be Empty");
            
            RuleFor(x => x.Request.Password)
                 .NotNull()
                 .NotEmpty()
                 .MinimumLength(8)
                 .Matches("[A-Z]").
                  WithMessage("Password must contain at least one uppercase letter")
                 .Matches("[0-9]")
        .         WithMessage("Password must contain  at least one digit");

            RuleFor(x => x.Request.Country).
                NotNull()
                .NotEmpty()
                .WithMessage("Country Name Cannot Be Empty");
                


            RuleFor(x => x.Request.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("State Name Cannot Be Empty");
                

        }

       
       


    }
}
