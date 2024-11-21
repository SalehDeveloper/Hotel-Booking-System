using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetByRole
{
    public class GetByRoleQueryHandler : IRequestHandler<GetByRoleQuery, Result<List<UserResponseDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetByRoleQueryHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<List<UserResponseDto>>> Handle(GetByRoleQuery request, CancellationToken cancellationToken)
        {
            var usersbyRole = await _userManager.GetUsersInRoleAsync(request.Role);


            if (usersbyRole.Any())
            {
                var usersWithRoles = await Task.WhenAll(usersbyRole.Select(async user =>
                {
                    // Explicitly load the navigation properties if not already loaded
                    var userWithNavigationProperties = await _userManager.Users
                        .Include(u => u.Country)
                        .Include(u => u.State)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);

                    var userDto = _mapper.Map<UserResponseDto>(userWithNavigationProperties);
                    var role = await _userManager.GetRolesAsync(user);
                    userDto.Role = role.First();
                    userDto.Country = user.Country.Name;
                    userDto.State = user.State.Name;
                    userDto.Photo = user.PhotoName;
                    return userDto;
                }));
                return new Result<List<UserResponseDto>>(usersWithRoles.ToList(), 200, "Retrived Successfully");
            }

            return new Result<List<UserResponseDto>>(404, "No Active Users");

        }
    }
}
