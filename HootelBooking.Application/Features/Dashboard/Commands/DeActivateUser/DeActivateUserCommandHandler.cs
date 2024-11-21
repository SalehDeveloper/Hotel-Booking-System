using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.DeActivateUser
{
    public class DeActivateUserCommandHandler : IRequestHandler<DeActivateUserCommand, Result<ActivationUserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DeActivateUserCommandHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<ActivationUserResponseDto>> Handle(DeActivateUserCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return new Result<ActivationUserResponseDto>(404, "User Not Found");
            }

          
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var currentUserRoleLevel = await _userRepository.GetUserRoleLevelAsync(currentUser);
            var targetUserRole = await _userRepository.GetUserRoleLevelAsync(user);

            if (!_authorizationRepository.DoesAuthorizeRoleManagementForDeletingAccept(currentUserRoleLevel, targetUserRole))
            {
                throw new ForbiddenException("Access Denied, Contact Your Admin");
            }

            
            if (await _userRepository.DeActivateUserAsync(user.Id))
            {
                await _userRepository.SetSuccessfullActivationSettings(user, currentUser);

                var result = _mapper.Map<ActivationUserResponseDto>(user);
                return new Result<ActivationUserResponseDto>(result, 200, "DeActivated Successfully");
            }

            return new Result<ActivationUserResponseDto>(409, "User Already Deactivated");

        }
    }
}
