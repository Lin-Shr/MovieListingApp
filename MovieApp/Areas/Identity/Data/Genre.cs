using MovieApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Areas.Identity.Data
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string ?Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Movie> ?Movies { get; set; }
        public List<GenreMovie> ?GenreMovies { get; set; }

    }
}
