namespace MovieApp.Interfaces
{
    public interface IEmail
    {
        Task SendEmailAsync (string body);
    }
}
