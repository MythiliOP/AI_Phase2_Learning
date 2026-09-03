using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Repositories;

namespace Week1_Project_WithCopilot.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public Task<User?> GetAsync(CancellationToken cancellationToken) =>
        userRepository.GetAsync(cancellationToken);
}
