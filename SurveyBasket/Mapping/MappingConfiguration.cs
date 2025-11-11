using Mapster;
using SurveyBasket.Contracts.Requests.Auth;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Contracts.Requests.Users;
using SurveyBasket.Contracts.Responses.Users;
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

        config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
            .Map(dest => dest, src => src.user)
            .Map(dest => dest.Roles, src => src.roles);

        config.NewConfig<CreateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email)
            .Map(dest => dest.EmailConfirmed, src => true);
    }
}
