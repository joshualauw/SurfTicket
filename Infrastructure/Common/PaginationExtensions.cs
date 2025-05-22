using Microsoft.EntityFrameworkCore;

namespace SurfTicket.Infrastructure.Common
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, int size)
        {
            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

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
