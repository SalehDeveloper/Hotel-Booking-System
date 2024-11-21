using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordCommandHandler(IAuthRepository authRepository, UserManager<ApplicationUser> userManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Get user by email
            var user = await _userManager.FindByEmailAsync(request.Request.Email);
            if (user == null)
            {
                return new Result<string>(404, "Invalid payload");
            }

            // Check if the current password is correct
            if (!await _userManager.CheckPasswordAsync(user, request.Request.CurrentPassword))
            {
                return new Result<string>(404, "Invalid payload");
            }

            // Attempt to change the password
            var isPasswordChanged = await _authRepository.ChangePasswordAsync(user, request.Request.CurrentPassword, request.Request.NewPassword);
            if (!isPasswordChanged)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

            // Set success settings and return success message
            await _authRepository.SetSuccessfullChangePasswordSettings(user);
            return new Result<string>("Password Updated Successfully", 200, "Use your new password for login operation");







        }
    }
}
