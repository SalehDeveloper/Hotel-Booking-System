
using HootelBooking.Application.Exceptions;
using HootelBooking.Application.Models;
using HootelBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HootelBooking.API.Middlewares
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<JwtHelper> _jwt;
        private readonly IOptions<RoleHelper> _roleHelper;

        public CustomAuthorizationMiddleware(
            RequestDelegate next,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<JwtHelper> jwt,
            IOptions<RoleHelper> roleHelper)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
            _jwt = jwt;
            _roleHelper = roleHelper;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Bypass if endpoint has AllowAnonymous attribute
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
                return;
            }

           

           

            // Check for Authorization header
            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                throw new UnAuthorizeException("Missing Authorization header.");
                
             
            }

            // Validate the token
            var token = authorizationHeader.ToString().Split(' ')[1];
            ClaimsPrincipal? claimsPrincipal;
            try
            {
                claimsPrincipal = await ValidateTokenAsync(token);
                if (claimsPrincipal == null)
                {
                    throw new UnAuthorizeException("Invalid token.");
                  
               
                }
                context.User = claimsPrincipal;
            }
            catch (Exception)
            {
                throw new UnAuthorizeException("Error validating token.");
                
              
            }

            // Retrieve the user from the claims
            var userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            var user = await userService.FindByNameAsync(userName);
            if (user == null )
            {
                throw new UnAuthorizeException("User Not Found");
                
            }
            if (!user.IsActive)
            {
                throw new UnAuthorizeException("User Is InActive");

            }

            // Retrieve roles required by the endpoint
            var requiredRoles = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>()?.Roles?.Split(',')?.Select(r => r.Trim().ToUpper()).ToList();
            if (requiredRoles == null || !requiredRoles.Any())
            {
                await _next(context);
                return;
            }

            // Check if user has any required roles
            var userRoles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(roleClaim => roleClaim.Value.ToUpper()).ToList();
            if (!userRoles.Any(role => requiredRoles.Contains(role)))
            {

                throw new ForbiddenException("Insufficient permissions.");
      
            }

            // Proceed to the next middleware if all checks pass
            await _next(context);
        }

       

        private Task<ClaimsPrincipal?> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwt.Value.Issuer,
                    ValidAudience = _jwt.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Value.key))
                };

                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParams, out _);
                return Task.FromResult(claimsPrincipal);
            }
            catch
            {
                return Task.FromResult<ClaimsPrincipal?>(null);
            }
        }
    }

}
