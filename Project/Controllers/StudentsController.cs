using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private StudentService studentService { get; set; }


        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpGet("students-only")]
        [Authorize(Roles = "Student")]
        public ActionResult<string> HelloStudents()
        {
            return Ok("Hello students!");
        }

        [HttpGet("teacher-only")]
        [Authorize(Roles = "Teacher")]
        public ActionResult<string> HelloTeachers()
        {
            return Ok("Hello teachers!");
        }

        [HttpPost("/add")]
        [AllowAnonymous]
        public IActionResult Add(StudentAddDto payload)
        {
            var result = studentService.AddStudent(payload);

            if (result == null)
            {
                return BadRequest("Student cannot be added");
            }

            return Ok(result);
        }


        [HttpGet("/get-all")]
        [AllowAnonymous]
        public ActionResult<List<Student>> GetAll()
        {
            var results = studentService.GetAll();

            return Ok(results);
        }

        [HttpGet("/get/{studentId}")]
        [AllowAnonymous]
        public ActionResult<Student> GetById(int studentId)
        {
            var result = studentService.GetById(studentId);

            if (result == null)
            {
                return BadRequest("Student not fount");
            }

            return Ok(result);
        }

        [HttpPatch("edit-name")]
        [Authorize(Roles = "Teacher")]
        public ActionResult<bool> GetById([FromBody] StudentUpdateDto studentUpdateModel)
        {
            var result = studentService.EditName(studentUpdateModel);

            if (!result)
            {
                return BadRequest("Student could not be updated.");
            }

            return result;
        }

        [HttpPost("grades-by-course")]
        [Authorize(Roles = "Student,Teacher")]
        public ActionResult<GradesByStudent> Get_CourseGrades_ByStudentId([FromBody] StudentGradesRequest request)
        {
            var result = studentService.GetGradesById(request.StudentId, request.CourseType);
            return Ok(result);
        }

        [HttpGet("{classId}/class-students")]
        [Authorize(Roles = "Student,Teacher")]
        public IActionResult GetClassStudents([FromRoute] int classId)
        {
            var results = studentService.GetClassStudents(classId);

            return Ok(results);
        }

        [HttpGet("grouped-students")]
        [Authorize(Roles = "Student,Teacher")]
        public IActionResult GetGroupedStudents()
        {
            var results = studentService.GetGroupedStudents();

            return Ok(results);
        }
    }
}
