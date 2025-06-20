using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace SurfTicket.Infrastructure.Extensions
{
	public static class FilterExtension
	{
		public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string>? filters, List<string>? whitelistFields)
		{
			if (filters == null || whitelistFields == null)
			{
				return query;
			}

			foreach (var filter in filters)
			{
				if (!string.IsNullOrWhiteSpace(filter.Value) && !string.IsNullOrWhiteSpace(filter.Key))
				{
					var field = filter.Key;
					if (whitelistFields.Contains(field, StringComparer.OrdinalIgnoreCase))
					{
						query = query.Where($"{field}.ToLower().Contains(@0)", filter.Value.ToLower());
					}
				}
			}

			return query;
		}
	}
}
