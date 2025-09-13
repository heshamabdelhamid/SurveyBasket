using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Results;
using SurveyBasket.Errors;
using SurveyBasket.Persistence;
using System.Collections.Generic;

namespace SurveyBasket.Services.Results;

public class ResultService(ApplicationDbContext context) : IResultService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollId, CancellationToken cancellationToken = default)
    {
        var pollVotes = await _context.Polls
            .Where(x => x.Id == PollId)
            .Select(x => new PollVotesResponse(
                x.Title,
                x.Votes.Select(v => new VoteResponse(
                    $"{v.User.FirstName} {v.User.LastName}",
                    v.SubmitedOn,
                    v.VoteAnswers.Select(a => new QuestionAnswerResponse(
                        a.Question.Content,
                        a.Answer.Content
                    ))
                ))
            ))
            .SingleOrDefaultAsync(cancellationToken);

        return pollVotes is null
           ? Result.Failure<PollVotesResponse>(PollErrors.PollNotFound)
           : Result.Success(pollVotes);
    }

    public async Task<Result<IEnumerable<VotesPerDateResponse>>> GetVotesPerDayAsync(int PollId, CancellationToken cancellationToken = default)
    {
        var PollIsExists = await _context.Polls.AnyAsync(x => x.Id == PollId, cancellationToken);

        if (!PollIsExists)
            return Result.Failure<IEnumerable<VotesPerDateResponse>>(PollErrors.PollNotFound);

        var VotesPerDay = await _context.Votes
            .Where(x => x.Id == PollId)
            .GroupBy(v => new { VoteDate = DateOnly.FromDateTime(v.SubmitedOn) })
            .Select(g => new VotesPerDateResponse(
                g.Key.VoteDate,
                g.Count()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<VotesPerDateResponse>>(VotesPerDay);
    }

    public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollId, CancellationToken cancellationToken = default)
    {
        var PollIsExists = await _context.Polls.AnyAsync(x => x.Id == PollId, cancellationToken);

        if (!PollIsExists)
            return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollErrors.PollNotFound);

        var votesPerQuestion = await _context.VoteAnswers
            .Where(v => v.Id == PollId)
            .Select(v => new VotesPerQuestionResponse(
                v.Question.Content,
                v.Question.Votes
                .GroupBy(v => new { 
                    AnswerId = v.Answer.Id,
                    AnswerContent = v.Answer.Content
                })
                .Select(g => new VotesPerAnswerResponse(
                    g.Key.AnswerContent,
                    g.Count()
                ))
             ))
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);
    }
}
