using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Services;

public interface ICustomerSearchService
{
    Task<IReadOnlyList<Customer>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken);
}
