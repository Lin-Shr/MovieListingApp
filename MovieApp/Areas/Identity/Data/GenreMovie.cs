using System.ComponentModel.DataAnnotations;

namespace MovieApp.Areas.Identity.Data
{
    public class GenreMovie
    {
        [Key]
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
        
        [Key]
        public int GenreId { get; set; }
        public Genre Genres { get; set; }
    }
}
