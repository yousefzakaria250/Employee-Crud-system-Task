using CORE_Layer.Dtos;
using CORE_Layer.Services;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User_Manager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userservice;

        public AccountController(IUserService userservice)
        {
            _userservice = userservice;
           
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userDTO)
        {
            if (ModelState.IsValid)
            {
                var authenticationModel = await _userservice.RegisterAsync(userDTO);
                if (authenticationModel.IsAuthenticated)
                {
                    return Ok(new { Token = authenticationModel.Token, Expiration = authenticationModel.ExpiresOn });
                }

                return
                    BadRequest(authenticationModel.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userDTO)
        {
            if (ModelState.IsValid)
            {
                var authenticationModel = await _userservice.Login(userDTO);
                if (authenticationModel.IsAuthenticated)
                {
                    return Ok(new { Token = authenticationModel.Token, Expiration = authenticationModel.ExpiresOn });
                }
                return
                    BadRequest(authenticationModel.Message);
            }
            return BadRequest(ModelState);
        }


        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<GetUserDto>> GetUser(string id)
        {
            var res = await _userservice.Get(id);
            if (res == null)
                return NotFound("Can`t Find This User");
            return Ok(res);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _userservice.GetAllRoles();
            return Ok(result);
        }


    }
}
