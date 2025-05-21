using Microsoft.EntityFrameworkCore;

namespace SurfTicket.Application.Common
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, 
            int page, 
            int size, 
            CancellationToken cancellationToken = default
        ) {
            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                Page = page,
                Size = size
            };
        }
    }
}
