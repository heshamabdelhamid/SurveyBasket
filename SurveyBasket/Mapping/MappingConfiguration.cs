using Mapster;
using SurveyBasket.Contracts.Requests.Auth;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<CreateQuestionRequest, Question>()
        //    .Ignore(nameof(Question.Answers));

        config.NewConfig<CreateQuestionRequest, Question>()
            .Map(
                dest => dest.Answers,
                src => src.Answers.Select(answer => new { Content = answer })
            );

        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);
    }
}
