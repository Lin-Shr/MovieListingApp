using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Areas.Identity.Data;

namespace MovieApp.Models
{
    public class MovieGenreVM
    {
        public Movie Movie { get; set; }
        public List<int> Genre { get; set; }
        public List<SelectListItem> Genres { get; set; }
        /*public SelectList Genres { get; set; }*/


    }
}
