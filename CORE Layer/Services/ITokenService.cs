using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateJwtToken(AppUser user);

    }
}
