using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MovieApp.Areas.Identity.Data
{
    public class Movie
    {
      /*  public Movie()
        {
            Genres = new List<Genre>();
        }*/
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Director { get; set; }
        public bool IsDeleted { get; set; } = false;
        [NotMapped]
        public Review Review { get; set; }

        public ICollection<Genre> Genres { get; set; }
   

        [AllowNull]
        public ICollection<Review> Reviews { get; set; }
        public List<GenreMovie> genreMovies { get; set; }
    }
}
