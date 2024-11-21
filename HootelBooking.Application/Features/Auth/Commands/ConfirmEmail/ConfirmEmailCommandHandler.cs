using HootelBooking.Application.Contracts;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailCommandHandler(IAuthRepository authRepository, UserManager<ApplicationUser> userManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
           
            var user = await _userManager.FindByEmailAsync(request.Request.Email);
            if (user == null)
            {
                return new Result<string>(404, "Invalid Email-Address");
            }

            
            if (!_authRepository.DoesEmailConfirmationCodeValid(user, request.Request.Code))
            {
                return new Result<string>(404, "The Email-Confirmation code is invalid or expired. Please request a new code.");
            }

            await _authRepository.SetSuccessfullRegisterationSettings(user);
            return new Result<string>("Email Confirmed Successfully", 200, "Complete Your Login Process");


        }




    }
}
