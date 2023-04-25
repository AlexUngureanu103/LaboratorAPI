using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class GradeAddDto
    {
        [Required]
        public double value { get; set; }

        [Required]
        public CourseType courseType { get; set; }

        [Required]
        public int StudentId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
