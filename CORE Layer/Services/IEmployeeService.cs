using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Specification.Employee_Specs;
using Db_Builder.Models.User_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Services
{
    public interface IEmployeeService
    {
        Task<Response<AppUser>> Add(AddUserDto userDTO , string UserId);

        Task<List<GetUserDto>> GetAllUsers();
        Task<List<DegreeState>> GetEmployeeStates();
        Task<Pagintation<GetUserDto>> GetAllWithSpecs(EmployeeSpecParams serviceSpec);
        Task<List<GetUserDto>> GetAllUsersWithId(EmployeeSpecParams serviceSpec  , string UserId);

        Task<Response<AppUser>> UpdateUser(UpdateUserDto userDTO);
        Task<Response<AppUser>> DeleteUser(string userId);

    }
}
