using Microsoft.EntityFrameworkCore;
using SurfTicket.Infrastructure.Common;

namespace SurfTicket.Infrastructure.Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PagedData<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, int size)
        {
            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var pagedResult = new PagedData<T>
            {
                Items = items,
                TotalItems = totalItems,
                Page = page,
                Size = size,
            };

            if (page > pagedResult.TotalPages)
            {
                pagedResult.Page = pagedResult.TotalPages;
            }

            return pagedResult;
        }
    }
}
