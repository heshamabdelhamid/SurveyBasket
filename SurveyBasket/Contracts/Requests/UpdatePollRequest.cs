namespace SurveyBasket.Contracts.Requests;

public class UpdatePollRequest
{
    public string Title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public bool Published { get; set; }
    public DateOnly StartsAt { get; set; }
    public DateOnly EndsAt { get; set; }
    
}