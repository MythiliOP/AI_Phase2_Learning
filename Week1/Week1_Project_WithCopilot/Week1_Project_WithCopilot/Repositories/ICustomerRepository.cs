using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken);
}
