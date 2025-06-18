namespace SurveyBasket.Contracts.Requests
{
    public class CreatePollRequest
    {
        public string Title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public bool Published { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
        
    }

    //public record CreatePollRequest(string Title, string Description);
}