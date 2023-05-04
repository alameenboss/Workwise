using System.Security.Claims;

namespace Workwise.Web.Helper
{
    public static class WorkwiseExtentions
    {
        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            if (claims == null)
            {
                return null;
            }

            var userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return null;
            }

            return userIdClaim.Value;
        }
    }
}