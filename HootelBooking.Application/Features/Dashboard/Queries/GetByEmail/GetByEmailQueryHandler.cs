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

namespace HootelBooking.Application.Features.Dashboard.Queries.GetByEmail
{
    public class GetByEmailQueryHandler : IRequestHandler<GetByEmailQuery, Result<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetByEmailQueryHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<UserResponseDto>> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.AsNoTracking().Where(x => x.Email == request.Email).Include(x => x.State).Include(x => x.Country).FirstOrDefaultAsync();

            if (user != null)
            {
                var userRoel = await _userManager.GetRolesAsync(user);

                var mappedUser = _mapper.Map<UserResponseDto>(user);
                mappedUser.Role = userRoel.First();
                mappedUser.State = user.State.Name;
                mappedUser.Country = user.Country.Name;
                mappedUser.Photo = user.PhotoName;

                return new Result<UserResponseDto>(mappedUser, 200, "Retrived Successfully");
            }
            return new Result<UserResponseDto>(404, "Not Found");
        }
    }
}
