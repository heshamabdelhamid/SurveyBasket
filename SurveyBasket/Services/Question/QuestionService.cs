using Mapster;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Contracts.Responses.Answer;
using SurveyBasket.Contracts.Responses.Question;
using SurveyBasket.Errors;
using SurveyBasket.Persistence;

namespace SurveyBasket.Services.Question;

public class QuestionService(ApplicationDbContext context) : IQuestionService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int PollId, CancellationToken cancellationToken = default)
    {
        var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId, cancellationToken);

        if (!pollIsExists)
            return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound);

        var questions = await _context.Questions
            .Where(q => q.PollId == PollId)
            .Include(q => q.Answers)
            //.ProjectToType<QuestionResponse>()
            .Select(q => new QuestionResponse(
                q.Id,
                q.Content,
                q.Answers.Select(a => new AnswerResponse(
                    a.Id,
                    a.Content
                ))
            ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<QuestionResponse>>(questions);
    }

    public async Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollId, string userId, CancellationToken cancellationToken = default)
    {
        var pollIsExists = await _context.Polls.AnyAsync(x =>  x.Id == PollId
                && x.IsPublished
                && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
                && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow),
                cancellationToken
        );

        if (!pollIsExists)
            return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound);

        var hasVote = await _context.Votes.AnyAsync(x => x.PollId == PollId && x.UserId == userId, cancellationToken);

        if (hasVote)
            return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DuplicatedVote);

        var questions = await _context.Questions
            .Where(x => x.PollId == PollId && x.IsActive)
            .Include(x => x.Answers)
            .Select(q => new QuestionResponse(
                q.Id,
                q.Content,
                q.Answers.Where(a => a.IsActive).Select(a =>  new AnswerResponse(a.Id, a.Content))
            ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<QuestionResponse>>(questions);
    }

    public async Task<Result<QuestionResponse>> GetAsync(int PollId, int QuestionId, CancellationToken cancellationToken = default)
    {
        var question = await _context.Questions
            .Where(q => q.PollId == PollId && q.Id == QuestionId)
            .Include(q => q.Answers)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        return question is not null
            ? Result.Success(question.Adapt<QuestionResponse>())
            : Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);
    }

    public async Task<Result<QuestionResponse>> AddAsync(int PollId, CreateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var pollIsExists = await _context.Polls.AnyAsync(p => p.Id == PollId, cancellationToken);

        if (!pollIsExists)
            return Result.Failure<QuestionResponse>(PollErrors.PollNotFound);

        var questionIsExists = await _context.Questions
            .AnyAsync(x => x.Content == request.Content && x.PollId == request.PollId, cancellationToken);

        if (questionIsExists)
            return Result.Failure<QuestionResponse>(QuestionErrors.DuplicatedQuestionContent);

        var question = request.Adapt<Entities.Question>();
        question.PollId = PollId;

        await _context.Questions.AddAsync(question, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(question.Adapt<QuestionResponse>());
    }

    public async Task<Result> UpdateAsync(int PollId, int QuestionId, UpdateQuestionRequest questionRequest, CancellationToken cancellationToken = default)
    {
        var questionIsExists =  await _context.Questions
            .AnyAsync(x => x.PollId == PollId
                && x.Content == questionRequest.Content
                && x.Id != QuestionId,
                cancellationToken
            );

        if (questionIsExists)
            return Result.Failure(QuestionErrors.DuplicatedQuestionContent);

        var question = await _context.Questions.Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == QuestionId && x.PollId == PollId, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        question.Content = questionRequest.Content;

        //currenr answers
        var currentAnswers = question.Answers.Select(x => x.Content).ToList();
        
        //add new answers
        var newAnswers = questionRequest.Answers.Except(currentAnswers).ToList();

        newAnswers.ForEach(answer =>
        {
            question.Answers.Add(new Entities.Answer { Content = answer });
        });

        question.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = questionRequest.Answers.Contains(answer.Content);
        });

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(int PollId, int QuestionId, CancellationToken cancellationToken = default)
    {
        var question = await _context.Questions
                .SingleOrDefaultAsync(q => q.PollId == PollId 
                    && q.Id == QuestionId,
                    cancellationToken
                );

        if (question is null)
            return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);

        question.IsActive = !question.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}