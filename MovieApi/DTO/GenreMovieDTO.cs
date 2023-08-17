using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTO
{
    public class GenreMovieDTO
    {

        [Key]
        public int MovieId { get; set; }
        public MovieDTO Movies { get; set; }

        [Key]
        public int GenreId { get; set; }
        public GenreDTO Genres { get; set; }
    }
}
