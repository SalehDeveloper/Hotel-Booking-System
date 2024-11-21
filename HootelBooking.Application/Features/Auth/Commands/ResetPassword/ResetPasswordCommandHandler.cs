using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IAuthRepository _authRepository;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IAuthRepository authRepository)
        {
            _userManager = userManager;
            _authRepository = authRepository;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {


            var user = await _userManager.FindByEmailAsync(request.Request.Email);

            if (user == null)
            {
                return new Result<string>(404, "Invalid Email-Address");
            }

            if (!user.IsActive)
            {
                throw new ForbiddenException("Your account is inactive. Please contact support to reactivate your account.");
            }

            if (!_authRepository.DoesResetPasswordCodeValid(user, request.Request.Code))
            {
                return new Result<string>(404, "The reset password code is invalid or expired. Please request a new code.");
            }

            var isPasswordChanged = await _authRepository.ResetPasswordAsync(user, request.Request.NewPassword);

            if (!isPasswordChanged)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

            await _authRepository.SetSuccessfullResetPasswordSettings(user);

            return new Result<string>("Password Reset Successfully", 200, "Complete your login via the new Password");




        }

        

    }
}
