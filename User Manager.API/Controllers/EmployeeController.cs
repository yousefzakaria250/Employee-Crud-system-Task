using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Services;
using CORE_Layer.Specification.Employee_Specs;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace User_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService; 
        private readonly UserManager<AppUser> _userManager;
        public EmployeeController(IEmployeeService employeeService, IUserService userService, UserManager<AppUser> userManager)
        {
            _employeeService = employeeService;
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize(Roles ="User")]
        [HttpPost("AddUser")]
        public async Task<IActionResult> Add([FromBody] AddUserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(email);
                var userId = await _userService.Get(user.Id);
                var result = await _employeeService.Add(UserDto, userId.Id);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
       // [Authorize(Roles="Admin")]
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<GetUserDto>> GetAll([FromQuery] EmployeeSpecParams spec)
        {
            var result = await _employeeService.GetAllWithSpecs(spec);
            if (result == null)
                return Ok(new Response<AppUser>(404, "No Users yet"));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllUserWithSupervisior")]
        public async Task<ActionResult<GetUserDto>> GetAllWitId([FromQuery] EmployeeSpecParams spec)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if( role == "Admin")
                 return Ok( await _employeeService.GetAllWithSpecs(spec) );
            var user = await _userManager.FindByEmailAsync(email);
            var userId = await _userService.Get(user.Id);
            var result = await _employeeService.GetAllUsersWithId(spec , userId.Id);
            if (result == null)
                return Ok(new Response<AppUser>(404, "No Users yet"));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("EmployeeState (Graduated / UnGraduated)")]
        public async Task<ActionResult<DegreeState>> EmployeeState()
        {
            return Ok(await _employeeService.GetEmployeeStates());
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            var res = await _employeeService.UpdateUser(userDto);
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("DeleteUser")]
       
        public async Task<ActionResult<AddUserDto>> DeleteUser(string id)
        {
            var result = await _employeeService.DeleteUser(id);
            return Ok(result);

        }
    }
}
