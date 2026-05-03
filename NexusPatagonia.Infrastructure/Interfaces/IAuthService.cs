namespace NexusPatagonia.Infrastructure.Interfaces
{
    public interface IAuthService
    {

        Task<string> AuthenticateAsync(string username, string password);

        string HashPassword(string password);
    }
}
