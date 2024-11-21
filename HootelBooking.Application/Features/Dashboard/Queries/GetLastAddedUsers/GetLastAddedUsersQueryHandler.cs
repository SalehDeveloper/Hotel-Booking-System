using AutoMapper;
using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Dashboard.Queries.GetLastAddedUsers
{
    public class GetLastAddedUsersQueryHandler : IRequestHandler<GetLastAddedUsersQuery, Result<List< AuthResponseDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetLastAddedUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<AuthResponseDto>>> Handle(GetLastAddedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetLastAddedUsers(request.Days);

            if (users.Any())
            {
                var mappedUsers = users.Select(u =>
                {
                    var useDto = _mapper.Map<AuthResponseDto>(u);
                    useDto.Photo = u.PhotoName; 
                    return useDto;
                    

                } 
                ).ToList();
               
                 

                return new Result<List<AuthResponseDto>>(mappedUsers, 200, "Retrived Successfuylly");
            
                   
            }

            return new Result<List<AuthResponseDto>>(404, "No Users Found");
        
        
        
        }
    }
}
