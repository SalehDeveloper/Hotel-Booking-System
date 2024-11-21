using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Dtos.User.Response;
using HootelBooking.Application.Models;
using MediatR;


namespace HootelBooking.Application.Features.Dashboard.Queries.GetAll
{
    public class GetAllQuery : IRequest<Result<List<UserResponseDto>>>
    {

    }
}
