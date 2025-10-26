namespace SurveyBasket.Contracts.Requests.Users.Profile;

public record UpdateProfileRequest(
    string FirstName,
    string LastName
);
