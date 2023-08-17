using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MovieApi.DTO
{
    public class UserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
