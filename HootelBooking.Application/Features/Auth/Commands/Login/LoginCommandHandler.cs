using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace HootelBooking.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<loginResponseDto>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MessageService _messageService;

        public LoginCommandHandler(IAuthRepository authRepository, UserManager<ApplicationUser> userManager, MessageService messageService)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _messageService = messageService;
        }

        public async Task<Result<loginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            


            var user = await _userManager.FindByEmailAsync(request.Request.Email);
            if (user == null)
            {
                return new Result<loginResponseDto>(404, "Invalid Payload");
            }

          
            if (!user.EmailConfirmed)
            {
                return new Result<loginResponseDto>(403, "Please Confirm Your Account First then try to login");
            }

           
            if (!await _authRepository.IsPasswordCorrect(user, request.Request.Password))
            {
                if (await _authRepository.IsAccountLockedOut(user))
                    return new Result<loginResponseDto>(429, "Your Account has been Locked. Please Try After 5 Minutes");

                return new Result<loginResponseDto>(400, "Invalid Payload");
            }

            if (!user.IsActive)
            {
                throw new ForbiddenException("Your account is inactive. Please contact support to reactivate your account.");
            }

           
            if (user.TwoFactorEnabled)
            {
                var code = CodeGenerationService.GenerateSecure8DigitCode();
                await _authRepository.SetLogin2FactorCodeSettings(user, code);
                await _messageService.SendMessage(user, code, "Two Factor Code Confirmation", "confirm 2factor code", "2");
                return new Result<loginResponseDto>(null, 200, "2FA code sent, please verify");
            }

            
            var token = await _authRepository.LoginAsync(user.Email);
            await _authRepository.SetSuccessfullLoginSettings(user);

            
            var userResponse = new loginResponseDto()
            {
                Token = token,
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return new Result<loginResponseDto>(userResponse, 200, "Login Successfully");



        }

        
      


    }
}
