using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private static readonly User CurrentUser =
        new(1, "Alex Johnson", "alex.johnson@example.com");

    public Task<User?> GetAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<User?>(CurrentUser);
    }
}
