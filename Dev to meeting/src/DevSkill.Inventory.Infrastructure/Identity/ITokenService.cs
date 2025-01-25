using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.Identity
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}
