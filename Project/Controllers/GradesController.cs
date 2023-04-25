using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradesController : ControllerBase
    {
        private GradeService gradeService { get; set; }

        public GradesController(GradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        [HttpPost("/add-grades")]
        public IActionResult Add(GradeAddDto payload)
        {
            var result = gradeService.AddGrade(payload);

            if (result == null)
            {
                return BadRequest("Grade cannot be added");
            }

            return Ok(result);
        }
    }
}
