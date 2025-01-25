using System.Security.Claims;

namespace DataAccess.Identity
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}
