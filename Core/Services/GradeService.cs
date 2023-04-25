using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;
        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public GradeAddDto AddGrade(GradeAddDto payload)
        {
            if (payload == null) return null;
            Grade newGrade = new()
            {
                Course = payload.courseType,
                Value = payload.value,
                StudentId = payload.StudentId,
                //Student = unitOfWork.Students.GetById(payload.StudentId),
                DateCreated = payload.DateCreated
            };

            unitOfWork.Grades.Insert(newGrade);
            unitOfWork.SaveChanges();

            return payload;
        }
    }
}
