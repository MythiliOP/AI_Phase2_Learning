using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Services;

public interface IUserService
{
    Task<User?> GetAsync(CancellationToken cancellationToken);
}
