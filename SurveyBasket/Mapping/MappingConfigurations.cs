using Mapster;
using SurveyBasket.Contracts.Responses;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<Poll, PollResponse>().Map(dest => dest.Notes, src => src.Description);

            config.NewConfig<Student, StudentResponse>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(
                    dest => dest.age,
                    src => src.DateOfBirth!.Value.Year,
                    srcCond => srcCond.DateOfBirth.HasValue
                );
        }
    }
}