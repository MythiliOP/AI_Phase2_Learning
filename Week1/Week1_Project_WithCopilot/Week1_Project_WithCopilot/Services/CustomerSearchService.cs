using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Repositories;

namespace Week1_Project_WithCopilot.Services;

public sealed class CustomerSearchService(ICustomerRepository customerRepository) : ICustomerSearchService
{
    public Task<IReadOnlyList<Customer>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken) =>
        customerRepository.SearchAsync(query, limit, cancellationToken);
}
