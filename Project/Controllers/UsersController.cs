using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase
    {
        private UserService usersService { get; set; }

        public UsersController(UserService usersService)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost("/add-user")]
        public IActionResult Add(UserAddDto payload)
        {
            UserAddDto result = usersService.AddUser(payload);

            if(result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("/get-user/{userId}")]
        public ActionResult<User> GetUserById(int userId)
        {
            var result = usersService.GetById(userId);

            if(result == null)
            {
                return BadRequest("Student not found");
            }

            return Ok(result);
        }

        [HttpGet("/get-all-users")]
        public ActionResult<List<User>> GetAllUsers()
        {
            var results = usersService.GetAll();

            return Ok(results);
        }
    }
}
