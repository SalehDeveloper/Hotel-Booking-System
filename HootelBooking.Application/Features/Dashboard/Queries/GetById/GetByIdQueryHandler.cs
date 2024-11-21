using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<UserResponseDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.AsNoTracking().Where(x => x.Id == request.Id).Include(x => x.Country).Include(x => x.State).FirstOrDefaultAsync();


            if (user == null)
                return new Result<UserResponseDto>(404, $"User With Id: {request.Id} Not Found");

            var userRole = await _userManager.GetRolesAsync(user);
            var mappedUser = _mapper.Map<UserResponseDto>(user);
            mappedUser.Role = userRole.First();
            mappedUser.State = user.State.Name;
            mappedUser.Country = user.Country.Name;
            mappedUser.Photo = user.PhotoName;

            return new Result<UserResponseDto>(mappedUser, 200, "Retrived Successfully");



        }
    }
}
