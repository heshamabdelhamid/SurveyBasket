using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Polls;
using SurveyBasket.Contracts.Responses.Polls;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using SurveyBasket.Mapping;
using SurveyBasket.Persistence;

namespace SurveyBasket.Services.Polls;

public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);
        return poll is not null
            ? Result.Success(poll.ToResponse())
            : Result.Failure<PollResponse>(PollErrors.PollNotFound);
    }

    public async Task<Result<PollResponse>> AddAsync(CreatePollRequest request, CancellationToken cancellationToken)
    {
        var newPoll = request.ToEntity();

        await _context.Polls.AddAsync(newPoll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(newPoll.ToResponse());
    }

    public async Task<Result> UpdateAsync(int id, UpdatePollRequest poll, CancellationToken cancellationToken)
    {
        var existingPoll = await _context.Polls.FindAsync(id, cancellationToken);

        if (existingPoll is null)
            return Result.Failure(PollErrors.PollNotFound);

        existingPoll.Title = poll.Title;
        existingPoll.Summary = poll.Description;
        existingPoll.StartsAt = poll.StartsAt;
        existingPoll.EndsAt = poll.EndsAt;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        _context.Remove(poll);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        poll.IsPublished = !poll.IsPublished;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}