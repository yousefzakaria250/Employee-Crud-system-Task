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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userservice;
        public AccountController(IUserService service)
        {
            _userservice = service;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> Add([FromBody] AddUserDto UserDto)
        {
            var result = await _userservice.Add(UserDto);
            return Ok(result);
        }


        [HttpGet("GetAllUser")]
        public async Task<ActionResult<GetUserDto>> GetAll([FromQuery] EmployeeSpecParams spec)
        {
            var result = await _userservice.GetAllWithSpecs(spec);
            if (result == null)
                return Ok(new Response<AppUser>(404, "No Users yet"));
            return Ok(result);
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<GetUserDto>> GetUser(string id)
        {
            var res = await _userservice.Get(id);
            if (res == null)
                return NotFound("Can`t Find This User");
            return Ok(res);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            var res = await _userservice.UpdateUser(userDto);
            return Ok(res);
        }

        [HttpPatch("DeleteUser")]
       
        public async Task<ActionResult<AddUserDto>> DeleteUser(string id)
        {
            var result = await _userservice.DeleteUser(id);
            return Ok(result);

        }
    }
}
