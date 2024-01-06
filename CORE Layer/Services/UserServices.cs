using AutoMapper;
using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Specification.Employee_Specs;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;



        public UserServices(IMapper mapper, IGenericRepository<AppUser> repository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _repository = repository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }


        public async Task<AuthResponse> Login(LoginDto userDto)
        {
            var AuthModel = new AuthResponse();
            AppUser user = await _userManager.FindByNameAsync(userDto.Email);
            if (user == null)
            {
                AuthModel.Message = " Email or Password is not correct";
                return AuthModel;
            }
            else
            {
                bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                if (found){
                    var myToken = await _tokenService.CreateJwtToken(user);
                    var roleList = await _userManager.GetRolesAsync(user);
                    AuthModel.IsAuthenticated = true;
                    AuthModel.Token = new JwtSecurityTokenHandler().WriteToken(myToken);
                    AuthModel.Email = user.Email;
                    AuthModel.Name = user.Name;
                    AuthModel.ExpiresOn = myToken.ValidTo;
                    return AuthModel;
                }
                else{
                        AuthModel.Message = "Password is not correct";
                        return AuthModel;
                    }
            }
        }


        public async Task<AuthResponse> RegisterAsync(RegisterDto userDTO)
        {
            //check if user has email registered before
            if (await _userManager.FindByEmailAsync(userDTO.Email) is not null)
                return new AuthResponse { Message = "Email Used Before ." };
            //check if any user uses the same UserName
            AppUser user = new AppUser();
            user.Id = Guid.NewGuid().ToString();
            user.Name = userDTO.Name;
            user.UserName = userDTO.Name;
            user.Email = userDTO.Email;
            user.PasswordHash = userDTO.Password;
            //create user in database

            IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
            {
                // assign  new user to Role As user
                result = await _userManager.AddToRoleAsync(user, userDTO.UserRole);
                if (!result.Succeeded)
                    return new AuthResponse { Message = "Not Role Added" };

                var jwtSecurityToken = await _tokenService.CreateJwtToken(user);
                return new AuthResponse
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Role = userDTO.UserRole ,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Name = user.Name
                };
            }

            return new AuthResponse { Message = "User Not Added" };

        }

        public async Task<GetUserDto> Get(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            var result = _mapper.Map<AppUser, GetUserDto>(user);
            return result;
        }

        public async Task<Response<List<IdentityRole>>> GetAllRoles()
        {
            var result = await _roleManager.Roles.ToListAsync();

            if (result == null)
                return new Response<List<IdentityRole>>(404, "No Roles yet");
            return new Response<List<IdentityRole>>(result);

        }
    }

    }
