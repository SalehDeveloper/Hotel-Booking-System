using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace HootelBooking.Application.Features.Dashboard.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Result<List<UserResponseDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IUserRepository userRepository, IAuthorizationRepository authorizationRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _authorizationRepository = authorizationRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<List<UserResponseDto>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {



            var users = await _userManager.Users.AsNoTracking().Include(x => x.Country).Include(x => x.State).ToListAsync();

            if (users.Any())
            {
                var mappedUsers = new List<UserResponseDto>();
                foreach (var user in users)
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
            return new Result<List<UserResponseDto>>(404, "No Users Found");



        }
    }
}
