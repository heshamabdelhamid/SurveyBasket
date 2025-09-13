namespace SurveyBasket.Entities;
   
public sealed class Question : AuditableEntity
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public int PollId { get; set; }
    public Poll Poll { get; set; } = null!;
    public ICollection<Answer> Answers { get; set; } = [];
    public ICollection<VoteAnswer> Votes { get; set; } = [];
}
