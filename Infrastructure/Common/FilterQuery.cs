using System.ComponentModel.DataAnnotations;

namespace SurfTicket.Infrastructure.Common
{
    public class FilterQuery
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 1000)]
        public int Size { get; set; } = 15;
        public string SortOrder { get; set; } = "asc";
        public string SortBy { get; set; } = string.Empty;
        public Dictionary<string, string>? FilterBy { get; set; }
    }
}
