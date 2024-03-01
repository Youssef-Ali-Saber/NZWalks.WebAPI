using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalks.WebAPI.Repositories
{
    public interface ITokenRepositery
    {
        Task<JwtSecurityToken> CreateJWTToken(IdentityUser user);
    }
}
