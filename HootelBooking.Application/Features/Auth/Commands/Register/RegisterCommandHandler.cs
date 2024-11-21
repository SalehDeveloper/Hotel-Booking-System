using AutoMapper;

using HootelBooking.Application.Contracts;
using HootelBooking.Application.Dtos.Auth.Response;
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Application.Services;
using HootelBooking.Domain.Entities;
using HootelBooking.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace HootelBooking.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly MessageService _messageService;
        private readonly ImageService _imageService;

        public RegisterCommandHandler(IAuthRepository authRepository, UserManager<ApplicationUser> userManager, ICountryRepository countryRepository, IStateRepository stateRepository, IMapper mapper, MessageService messageService, ImageService imageService)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
            _messageService = messageService;
            _imageService = imageService;
        }

        public async Task<Result<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //1- register user
            //2- Generate EmailConfirmation Token
            //3- Send EmilConfirmation Message 


            var user = await _userManager.FindByEmailAsync(request.Request.Email);

            if (user != null)
            {
                return new Result<AuthResponseDto>(404, "Invalid Email-Address");
            }

            if (!await _countryRepository.DoesCountryExist(request.Request.Country))
            {
                return new Result<AuthResponseDto>(404, "Invalid Country");
            }

            if (!await _countryRepository.DoesStateBelongsToCountry(request.Request.State, request.Request.Country))
            {
                return new Result<AuthResponseDto>(404, "Invalid State");
            }

            var country = await _countryRepository.GetByName(request.Request.Country);
            var state = await _stateRepository.GetByNameAsync(request.Request.State);

            // Mapping the User
            var userToAdd = _mapper.Map<ApplicationUser>(request.Request);
            userToAdd.CountryID = country.Id;
            userToAdd.StateID = state.Id;
            userToAdd.SecurityStamp = Guid.NewGuid().ToString();
            userToAdd.CreatedBy = "system";
            userToAdd.PhotoName = await _imageService.UploadImageAsync(request.Request.Photo);

            // Register user
            var result = await _authRepository.RegisterAsync(userToAdd, request.Request.Password);
            if (result == null)
            {
                throw new ErrorResponseException(500, "Failed To Register", "Internal Server Error");
            }

            var userWithCountryAndState = await _userManager.Users
                .Include(u => u.Country)
                .Include(u => u.State)
                .FirstOrDefaultAsync(u => u.Id == result.Id);

            var code = CodeGenerationService.GenerateSecure8DigitCode();
            await _authRepository.SetRegisterCodeSettings(userWithCountryAndState, code);
            await _messageService.SendMessage(userWithCountryAndState, code, "Email Confirmation code", "confirm your account", "5");

            // Map to userResponseDto
            var mappedResult = _mapper.Map<AuthResponseDto>(userWithCountryAndState);
            mappedResult.Photo = userToAdd.PhotoName;

            return new Result<AuthResponseDto>(mappedResult, 200, "Please complete registration by entering the code sent to your email");





        }

        

    }
}
