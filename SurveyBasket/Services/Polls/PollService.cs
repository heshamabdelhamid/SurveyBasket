using Microsoft.EntityFrameworkCore;
using SurveyBasket.Entities;
using SurveyBasket.Persistence;

namespace SurveyBasket.Services.Polls
{
    public class PollService(SurveyBasketDbContext context) : IPollService
    {
        private readonly SurveyBasketDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Polls.FindAsync([id], cancellationToken);
        }

        public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken)
        {
            await _context.Polls.AddAsync(poll,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return poll;
        }

        public async Task<bool> UpdateAsync(int id, Poll poll,CancellationToken cancellationToken)
        {
            var existingPoll = await GetAsync(id,cancellationToken);
        
            if (existingPoll is null)
                return false;
        
            existingPoll.Title = poll.Title;
            existingPoll.Summary = poll.Summary;
            existingPoll.StartsAt = poll.StartsAt;
            existingPoll.EndsAt = poll.EndsAt;
            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var poll = await GetAsync(id,cancellationToken);
         
            if (poll is null)
                return false;
            
            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        
        public async Task<bool> TogglePublishAsync(int id, CancellationToken cancellationToken)
        {
            var poll = await GetAsync(id, cancellationToken);

            if (poll is null)
                return false;
            
            poll.IsPublished = !poll.IsPublished;
            await _context.SaveChangesAsync(cancellationToken);
            
            return  true;
        }
        
    }
}
