using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public sealed class InMemoryCustomerRepository : ICustomerRepository
{
    private static readonly Customer[] Customers =
    [
        new(1, "Alice Johnson", "alice.johnson@example.com"),
        new(2, "Bob Smith", "bob.smith@example.com"),
        new(3, "Carol Williams", "carol.williams@example.com")
    ];

    public Task<IReadOnlyList<Customer>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var normalizedQuery = query.Trim();

        IReadOnlyList<Customer> results = Customers
            .Where(customer =>
                customer.Name.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ||
                customer.Email.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
            .Take(limit)
            .ToArray();

        return Task.FromResult(results);
    }
}
