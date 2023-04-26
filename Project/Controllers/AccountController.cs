using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private UserService userService { get; set; }

        public AccountController(UserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("register/student")]
        public IActionResult RegisterUser(RegisterDto registerData)
        {
            userService.Register(registerData);
            return Ok();
        }
    }
}
