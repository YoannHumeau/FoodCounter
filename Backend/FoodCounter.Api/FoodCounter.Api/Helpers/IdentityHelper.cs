using FoodCounter.Api.Entities;
using System.Linq;

namespace FoodCounter.Api.Helpers
{
    /// <summary>
    /// Identity helper class
    /// </summary>
    public static class IdentityHelper
    {
        /// <summary>
        /// Get the role of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean that says user role is Admin or not</returns>
        public static bool IsUserAdmin(System.Security.Claims.ClaimsPrincipal user)
        {
            return Role.Admin == user?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        }
    }
}
