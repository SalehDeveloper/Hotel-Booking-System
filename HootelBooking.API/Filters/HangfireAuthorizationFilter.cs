using Hangfire.Dashboard;

using System.Net;
using System.Security.Claims;

namespace HootelBooking.API.Filters
{
    public class HangfireAuthorizationFilter:IDashboardAuthorizationFilter
    {
        private readonly string[] _allowedRoles;

        public HangfireAuthorizationFilter(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Check if the user is authenticated
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Check if the user has one of the required roles
            var userRoles = httpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            if ( userRoles.Any(role => _allowedRoles.Contains(role))) 
                return true;


            // Access denied, set response to Forbidden
            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            httpContext.Response.WriteAsync("You don't have permission to access this page");
            return false;
        }
    }
}
