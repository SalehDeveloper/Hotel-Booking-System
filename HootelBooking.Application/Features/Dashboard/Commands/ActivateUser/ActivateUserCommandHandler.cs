
using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace HootelBooking.Application.Features.Dashboard.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, Result<ActivationUserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ActivateUserCommandHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<ActivationUserResponseDto>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            // Retrieve user by ID
            var user = await _userRepository.GetByIdAsync(request.Id);

            // If user is not found, return a 404 error immediately
            if (user is null)
            {
                return new Result<ActivationUserResponseDto>(404, "User Not Found");
            }

            // Get current user and their role level
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var currentUserRoleLevel = await _userRepository.GetUserRoleLevelAsync(currentUser);
            var targetUserRole = await _userRepository.GetUserRoleLevelAsync(user);

            // Check if current user has authorization to manage the target user's role
            if (!_authorizationRepository.DoesAuthorizeRoleManagementForDeletingAccept(currentUserRoleLevel, targetUserRole))
            {
                throw new ForbiddenException("Access Denied, Contact Your Admin");
            }

            // If the user is already activated, return a 409 error immediately
            if (await _userRepository.ActivateUserAsync(user.Id))
            {
                await _userRepository.SetSuccessfullActivationSettings(user, currentUser);

                var result = _mapper.Map<ActivationUserResponseDto>(user);
                return new Result<ActivationUserResponseDto>(result, 200, "Activated Successfully");
            }

            return new Result<ActivationUserResponseDto>(409, "User Already Activated");
        }
    }
}
