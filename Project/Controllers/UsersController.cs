using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private UserService userService { get; set; }

        private StudentService studentService { get; set; }

        public UsersController(UserService userService, StudentService studentService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpPost("/register-user")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto payload)
        {
            RegisterDto result = userService.Register(payload);

            if (result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            string jwtToken = userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }


        [HttpGet("test-auth")]
        [AllowAnonymous]
        public IActionResult TestAuth()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var valid = userService.ValidateToken(token);

            ClaimsPrincipal user = User;

            string result = string.Empty;

            foreach (var claim in user.Claims)
            {
                result += claim.Type + " : " + claim.Value + '\n';
            }

            var hasRole_student = user.IsInRole("Student");
            var hasRole_teacher = user.IsInRole("Teacher");

            return Ok(result);
        }

        [HttpPost("all-grades/{studentId}")]
        [Authorize(Roles = "Student,Teacher")]
        public ActionResult<StudentGradesDto> GetStudentGrades(int studentId)
        {
            var result = studentService.GetStudentGrades(studentId);

            return Ok(result);
        }

        [HttpGet("/get-user/{userId}")]
        [AllowAnonymous]
        public ActionResult<User> GetUserById(int userId)
        {
            var result = userService.GetById(userId);

            if (result == null)
            {
                return BadRequest("Student not found");
            }

            return Ok(result);
        }

        [HttpGet("/get-all-users")]
        [AllowAnonymous]
        public ActionResult<List<User>> GetAllUsers()
        {
            var results = userService.GetAll();

            return Ok(results);
        }
    }
}
