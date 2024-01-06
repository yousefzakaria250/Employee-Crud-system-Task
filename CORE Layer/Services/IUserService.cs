using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Specification.Employee_Specs;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public interface IUserService
    {
        Task<AuthResponse> Login(LoginDto userDto);
        Task<AuthResponse> RegisterAsync(RegisterDto userDTO);
        Task<GetUserDto> Get(string id);
        Task<Response<List<IdentityRole>>> GetAllRoles();

    }
}
