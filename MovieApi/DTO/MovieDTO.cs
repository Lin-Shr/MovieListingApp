using MovieApp.Areas.Identity.Data;

namespace MovieApi.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Director { get; set; }
        //public List<GenreDisplayDTO> Genre { get; set; }
        //public List<GenreDTO> genres { get; set; }
    }
}
