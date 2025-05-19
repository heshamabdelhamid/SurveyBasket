using SurveyBasket.Contracts.Responses;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping
{
    public static class StudentMapping
    {
        public static StudentResponse ToResponse(this Student student)
        {
            return new StudentResponse
            {
                Id = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                age = student.DateOfBirth == null ? 0 : DateTime.Now.Year - student.DateOfBirth.Value.Year
            };
        }
    }
}