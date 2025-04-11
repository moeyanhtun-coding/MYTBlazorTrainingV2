using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MYTBlazorTraining.WebApi.Models.Login;
using MYTBlazorTraining.WebApi.Models.Register;
using MYTBlazorTraining.WebApi.Services;

namespace MYTBlazorTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseModel>> UserLogin(UserLoginModel user)
        {
            var result = await _auth.UserLoginAsync(user);  
            if(result.Flag) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponseModel>> UserRegister(UserRegisterModel user) 
        {
            var result = await _auth.UserRegisterAsync(user);
            if (result.Flag) return Ok(result);
            return BadRequest(result);
        }
    }
}
