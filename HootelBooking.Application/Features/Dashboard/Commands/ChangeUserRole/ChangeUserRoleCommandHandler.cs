using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, Result<ChangeUserRoleResponseDto>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeUserRoleCommandHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _userManager = userManager;
        }

        public async Task<Result<ChangeUserRoleResponseDto>> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(request.RequestDto.Id);
            if (user == null)
            {
                return new Result<ChangeUserRoleResponseDto>(404, "User Not Found");
            }

            
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var currentUserRoleLevel = await _userRepository.GetUserRoleLevelAsync(currentUser);

            
            Enum.TryParse<enRoles>(request.RequestDto.Role, true, out var role);
            var targetUserRoleLevel = (int)role;

            
            var targetUserBeforeChange = await _userRepository.GetUserRoleLevelAsync(user);
            if (targetUserBeforeChange == 1)
            {
                throw new ForbiddenException("Access Denied , You cannot change role for users");
            }


            if (currentUser.Id == request.RequestDto.Id)
            {
                throw new ForbiddenException("Access Denied, You Dont have permission to change your roles");
            }

            
            if (!_authorizationRepository.DoesAuthorizeRoleManagementForChangingRoleAccept(currentUserRoleLevel, targetUserRoleLevel))
            {
                throw new ForbiddenException("Access Denied, Contact Your Admin");
            }

            
            if (!await _userManager.IsInRoleAsync(user, request.RequestDto.Role))
            {
                await _userRepository.ChangeUserRoleAsync(user, request.RequestDto.Role);
                await _userRepository.SetSuccessfullChangeRoleSettings(user, currentUser);

                var response = new ChangeUserRoleResponseDto()
                {
                    Id = request.RequestDto.Id,
                    OldRole = ((enRoles)targetUserBeforeChange).ToString(),
                    NewRole = request.RequestDto.Role.ToUpper()
                };

                return new Result<ChangeUserRoleResponseDto>(response, 200, "Changed Successfully");
            }

            return new Result<ChangeUserRoleResponseDto>(429, "User Already In Role");
        }
    }
}
