using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(CancellationToken cancellationToken);
}
