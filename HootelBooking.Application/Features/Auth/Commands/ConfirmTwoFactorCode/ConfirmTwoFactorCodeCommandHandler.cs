using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HootelBooking.Application.Features.Auth.Commands.ConfirmTwoFactorCode
{
    public class ConfirmTwoFactorCodeCommandHandler : IRequestHandler<ConfirmTwoFactorCodeCommand, Result<loginResponseDto>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmTwoFactorCodeCommandHandler(IAuthRepository authRepository, UserManager<ApplicationUser> userManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<Result<loginResponseDto>> Handle(ConfirmTwoFactorCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Request.Email);
            if (user == null)
            {
                return new Result<loginResponseDto>(404, "Invalid Email-Address");
            }

            // Check if the user account is active
            if (!user.IsActive)
            {
                throw new ForbiddenException("Your account is inactive. Please contact support to reactivate your account.");
            }

            // Check if the 2-factor code is valid
            if (!_authRepository.Does2FactorCodeValid(user, request.Request.TwoFactorCode))
            {
                return new Result<loginResponseDto>(404, "The 2Factor code is invalid or expired. Please request a new code.");
            }

            // Process login and return success
            var token = await _authRepository.LoginAsync(user.Email);
            await _authRepository.SetSuccessfullLoginWith2FactorSettings(user);
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
