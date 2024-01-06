using CORE_Layer.Dtos;
using CORE_Layer.Helper;
using CORE_Layer.Services;
using CORE_Layer.Specification.Employee_Specs;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> Add([FromBody] AddUserDto UserDto)
        {
            var result = await _employeeService.Add(UserDto);
            return Ok(result);
        }


        [HttpGet("GetAllUser")]
        public async Task<ActionResult<GetUserDto>> GetAll([FromQuery] EmployeeSpecParams spec)
        {
            var result = await _employeeService.GetAllWithSpecs(spec);
            if (result == null)
                return Ok(new Response<AppUser>(404, "No Users yet"));
            return Ok(result);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            var res = await _employeeService.UpdateUser(userDto);
            return Ok(res);
        }

        [HttpPatch("DeleteUser")]
       
        public async Task<ActionResult<AddUserDto>> DeleteUser(string id)
        {
            var result = await _employeeService.DeleteUser(id);
            return Ok(result);

        }
    }
}
