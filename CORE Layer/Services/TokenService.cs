using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            //get claims
            var UserClaims = await _userManager.GetClaimsAsync(user);

            //get Role
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim("UserId" ,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
            }.Union(UserClaims).Union(roleClaims);
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secret"]));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //create Token
            JwtSecurityToken myToken = new JwtSecurityToken(
             issuer: _configuration["JWT:ValidIssuer"],
             audience: _configuration["JWT:ValidAudiance"],
             expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])).ToLocalTime(),
            claims: claims,
            signingCredentials: signingCred
         );
            return myToken;
        }
    }
}
