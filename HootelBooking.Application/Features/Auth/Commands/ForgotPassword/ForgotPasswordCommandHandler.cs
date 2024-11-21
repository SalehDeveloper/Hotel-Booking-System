using HootelBooking.Application.Contracts;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
         private readonly UserManager<ApplicationUser> _userManager;
         private readonly IAuthRepository _authRepository;
         private readonly MessageService _messageService;
        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, MessageService messageService, IAuthRepository authRepository)
        {
            _userManager = userManager;
            _authRepository = authRepository;
            
            _messageService = messageService;   
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Result<string>(404, "Invalid Email-Address");
            }

            // Check if the user account is active
            if (!user.IsActive)
            {
                throw new ForbiddenException("Your account is inactive. Please contact support to reactivate your account, then try to reset your password");
            }

            // Generate and send reset password code
            var code = CodeGenerationService.GenerateSecure8DigitCode();
            var isSettingsChanged = await _authRepository.SetForgotPasswordSettingsAsync(user, code);

            if (!isSettingsChanged)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

            await _messageService.SendMessage(user, code, "Reset Password code", "reset your password", "2");
            return new Result<string>("Code Sent Successfully", 200, "Please Check Your Email then Reset the password via code");


        }
    }
}
