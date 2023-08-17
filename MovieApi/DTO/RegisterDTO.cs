namespace MovieApi.DTO
{
    public class RegisterDTO
    {
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}
