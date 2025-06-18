namespace SurveyBasket.Contracts.Responses
{
    public class PollResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool Published { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
    }

    // this if you want to use record instead of class
    //public record PollResponse(int id, string Title, string Notes);
}