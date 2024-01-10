using AutoMapper;
using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Specification.Employee_Specs;
using Data_Access_Layer.Interfaces;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public class EmployeeService :IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _AppUserManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public EmployeeService(IMapper mapper, IGenericRepository<AppUser> repository, UserManager<AppUser> appUserManager, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _mapper = mapper;
            _repository = repository;
            _AppUserManager = appUserManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<Response<AppUser>> Add(AddUserDto userDTO , string UserId)
        {
            var AppUserExist = await _AppUserManager.FindByEmailAsync(userDTO.Email);
            if (AppUserExist != null)
                return new Response<AppUser>(405, "this email Already Exists!");
            var user = _mapper.Map<AddUserDto, AppUser>(userDTO);
          //  string Image = ConvertImage(userDTO.User_Image);
            user.SupervisiorId =  UserId;
           // user.User_Image = Image;
            await _unitOfWork.Repository<AppUser>().Add(user);
            await _unitOfWork.Complete();
            return new Response<AppUser>(200, "Employee added successfully");
        
        }


        public async Task<Response<AppUser>> DeleteUser(string userId)
        {
            var AppUser = await _AppUserManager.FindByIdAsync(userId);

            if (AppUser == null)
                return new Response<AppUser>(404, "this User does not exist");

            _unitOfWork.Repository<AppUser>().Delete(AppUser);
            await _unitOfWork.Complete();
            return new Response<AppUser>(200, "User is Deleted successfully");
        }

        public async Task<List<GetUserDto>> GetAllUsers()
        {
            var Employees = await _unitOfWork.Repository<AppUser>().GetAllAsync();
            var Result = _mapper.Map<List<GetUserDto>>(Employees);
            return Result;

        }

        public async Task<List<GetUserDto>> GetAllUsersWithId(EmployeeSpecParams serviceSpec , string UserId)
        {
            var spec = new EmployeeWithDegreeState(serviceSpec);
            var Countspec = new EmployeeWithFiltersForCountSpecs(serviceSpec);
            var totalCount = await _unitOfWork.Repository<AppUser>().Count(Countspec);
            var Employees = await _unitOfWork.Repository<AppUser>().GetData_ByExepressionAsync( U => U.SupervisiorId == UserId , new[] { "DegreeState" });
            var Result = _mapper.Map<List<GetUserDto>>(Employees);
            return Result;
        }


        public async Task<Pagintation<GetUserDto>> GetAllWithSpecs(EmployeeSpecParams serviceSpec)
        {
            var spec = new EmployeeWithDegreeState(serviceSpec);
            var Countspec = new EmployeeWithFiltersForCountSpecs(serviceSpec);
            var totalCount = await _unitOfWork.Repository<AppUser>().Count(Countspec);
            var Employees = await _unitOfWork.Repository<AppUser>().GetAllDataWithSpecAsync(spec);
            var mapping = _mapper.Map<List<GetUserDto>>(Employees);
            return new Pagintation<GetUserDto>(mapping, serviceSpec.PageSize, totalCount, serviceSpec.PageIndex);

        }

        public Task<List<DegreeState>> GetEmployeeStates()
        {
            return
            _unitOfWork.Repository<DegreeState>().GetAllAsync();
        }

        public async Task<Response<AppUser>> UpdateUser(UpdateUserDto userDTO)
        {
            var user = await _AppUserManager.FindByIdAsync(userDTO.Id);
            if (user == null)
                return new Response<AppUser>(404, "Can`t Find This Employee");

            //var result = _mapper.Map<UpdateUserDto,AppUser>(userDTO);
            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.PhoneNumber;
            user.DegreeStateId = userDTO.DegreeStateId;
            user.User_Image = userDTO.User_Image;

            _unitOfWork.Repository<AppUser>().Update(user);
            await _unitOfWork.Complete();
            return new Response<AppUser>(200, "User profile updated successfully");

        }

        public string ConvertImage( IFormFile image)
        {
            IFormFile file = image;
            string NewName = Guid.NewGuid().ToString() + file.FileName;
            FileStream fs = new FileStream(
                 Path.Combine(Directory.GetCurrentDirectory(),
                  "Content", "Images", NewName)
                 , FileMode.OpenOrCreate, FileAccess.ReadWrite);
            file.CopyTo(fs);
            fs.Position = 0;

            return NewName;
        }

    }
}
