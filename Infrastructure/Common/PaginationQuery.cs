using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Infrastructure.Common
{
    public class PaginationQuery
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 1000)]
        public int Size { get; set; } = 15;
    }
}
