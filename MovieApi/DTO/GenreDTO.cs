using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTO
{
    public class GenreDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
