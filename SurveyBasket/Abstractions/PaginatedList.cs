using Microsoft.EntityFrameworkCore;

namespace SurveyBasket.Abstractions
{
    public class PaginatedList<T>(List<T> items, int pageNumber, int count, int pageSize)
    {
        public List<T>? Items { get; private set; } = items;

        public int PageNumber { get; private set; } = pageNumber;

        public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)pageSize);

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> sourc, int pageNumberm, int pageSize,
            CancellationToken cancellationToken = default)
        {
            var count = await sourc.CountAsync(cancellationToken);
            var items = await sourc.Skip((pageNumberm - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, pageNumberm, count, pageSize);
        }
    }
}
