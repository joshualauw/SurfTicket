using System.Linq.Dynamic.Core;

namespace SurfTicket.Infrastructure.Extensions
{
	public static class SortExtension
	{
		public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy, string sortOrder, List<string> whitelistFields)
		{
			if (string.IsNullOrWhiteSpace(sortBy) || whitelistFields == null)
			{
				return query;
			}

			if (whitelistFields.Contains(sortBy, StringComparer.OrdinalIgnoreCase))
			{
				var direction = sortOrder == "asc" ? "ascending" : "descending";
				query = query.OrderBy($"{sortBy} {direction}");
			}

			return query;
		}
	}
}
