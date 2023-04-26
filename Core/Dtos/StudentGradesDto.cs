using DataLayer.Entities;

namespace Core.Dtos
{
    public class StudentGradesDto
    {
        public int? StudentId { get; set; }
        public string StudentCompleteName { get; set; }

        public List<GradeDto> Grades { get; set; } = new();

        public StudentGradesDto(Student student)
        {
            StudentId = student?.Id;
            StudentCompleteName = student?.FirstName + " " + student?.LastName;

            Grades = student.Grades
                .Select(g => new GradeDto
                {
                    Value = g.Value,
                    Course = g.Course
                })
                .ToList();
        }
    }
}
