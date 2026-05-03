using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ValidateUserPassword(string user, string password);

        Task<User?> GetByUsernameAsync(string username);
    }
}
