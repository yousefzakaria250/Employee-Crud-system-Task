using AutoMapper;
using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _AppUserManager;
        private readonly IUnitOfWork _unitOfWork;



        public UserServices(IMapper mapper, IGenericRepository<AppUser> repository, UserManager<AppUser> appUserManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _AppUserManager = appUserManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<AppUser>> Add(AddUserDto userDTO)
        {
            var AppUserExist = await _AppUserManager.FindByEmailAsync(userDTO.Email);
            if (AppUserExist != null)
                return new Response<AppUser>(405, "this email Already Exists!");

            var user = _mapper.Map<AddUserDto, AppUser>(userDTO);
            await _unitOfWork.Repository<AppUser>().Add(user);
            await _unitOfWork.Complete();
          
            return new Response<AppUser>(200, "Employee added successfully");
        }
    }
}
