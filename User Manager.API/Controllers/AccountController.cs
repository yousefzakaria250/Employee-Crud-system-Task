using CORE_Layer.Dtos;
using CORE_Layer.Services;
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
    }
}
