using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MovieApp.Areas.Identity.Data
{
    public class Review
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public int MovieId { get; set; }
        public Movie Movie {get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
