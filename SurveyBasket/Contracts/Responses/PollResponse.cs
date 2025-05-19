namespace SurveyBasket.Contracts.Responses
{
    public class PollResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }


    // this if you want to use record instead of class
    //public record PollResponse(int Id, string Title, string Notes);
}