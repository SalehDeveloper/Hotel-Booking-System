using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<UserResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(UserManager<ApplicationUser> userManager, IUserRepository userRepository, IAuthorizationRepository authorizationRepository, ICountryRepository countryRepository, IStateRepository stateRepository, ImageService imageService, IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<Result<UserResponseDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

          
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var currentUserRoleLevel = await _userRepository.GetUserRoleLevelAsync(currentUser);

      
            Enum.TryParse<enRoles>(request.User.Role, true, out var role);
            var targetUserRoleLevel = (int)role;

         
            if (targetUserRoleLevel == 1)
            {
                throw new ForbiddenException("Access Denied, You Cannot Add Users From This Point");
            }

          
            if (!_authorizationRepository.DoesAuthorizeRoleManagementForAddingAccept(currentUserRoleLevel, targetUserRoleLevel))
            {
                throw new ForbiddenException("Access Denied, Contact Your Admin");
            }

            var existingUser = await _userManager.FindByEmailAsync(request.User.Email);
            if (existingUser is not null)
            {
                return new Result<UserResponseDto>(404, "User Already Exists");
            }

       
            if (!await _countryRepository.DoesCountryExist(request.User.Country))
            {
                return new Result<UserResponseDto>(404, "Invalid Country");
            }

            if (!await _countryRepository.DoesStateBelongsToCountry(request.User.State, request.User.Country))
            {
                return new Result<UserResponseDto>(404, "Invalid State");
            }

      
            var country = await _countryRepository.GetByName(request.User.Country);
            var state = await _stateRepository.GetByNameAsync(request.User.State);

            
            var userToAdd = _mapper.Map<ApplicationUser>(request.User);
            userToAdd.CountryID = country.Id;
            userToAdd.StateID = state.Id;
            userToAdd.SecurityStamp = Guid.NewGuid().ToString();
            userToAdd.CreatedBy = currentUser.UserName;
            userToAdd.PhotoName = await _imageService.UploadImageAsync(request.User.Photo);

           
            var result = await _userManager.CreateAsync(userToAdd, request.User.Password);
            if (!result.Succeeded)
            {
                throw new ErrorResponseException(500, "Operation Failed", "Internal Server Error");
            }

           
            await _userManager.AddToRoleAsync(userToAdd, request.User.Role);

            var mappedUser = _mapper.Map<UserResponseDto>(userToAdd);
            mappedUser.Role = request.User.Role;
            mappedUser.Country = country.Name;
            mappedUser.Photo = userToAdd.PhotoName;

            return new Result<UserResponseDto>(mappedUser, 200, "Added Successfully");


        }
    }
}
