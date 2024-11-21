using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetInActive
{
    public class GetInActiveQueryHandler : IRequestHandler<GetInActiveQuery, Result<List<UserResponseDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetInActiveQueryHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<List<UserResponseDto>>> Handle(GetInActiveQuery request, CancellationToken cancellationToken)
        {

            var userWithNavigationProperties = await _userManager.Users.AsNoTracking()
                     .Include(u => u.Country)
                     .Include(u => u.State)
                     .Where(x => !x.IsActive).ToListAsync();

            if (userWithNavigationProperties.Any())
            {
                var mappedUsers = new List<UserResponseDto>();
                foreach (var user in userWithNavigationProperties)
                {
                    // Map ApplicationUser to UserResponseDto
                    var userDto = _mapper.Map<UserResponseDto>(user);

                    // Retrieve roles for each user individually
                    var roles = await _userManager.GetRolesAsync(user);
                    userDto.Role = roles.FirstOrDefault();
                    userDto.State = user.State?.Name;
                    userDto.Country = user.Country?.Name;
                    userDto.Photo = user.PhotoName;

                    mappedUsers.Add(userDto);
                }

                return new Result<List<UserResponseDto>>(mappedUsers.ToList(), 200, "Retrived Successfully");
            }
            return new Result<List<UserResponseDto>>(404, "No Item Founds");
        }
    }
}
