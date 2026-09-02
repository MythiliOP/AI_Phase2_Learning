namespace Week1_Project_WithCopilot;

public sealed record Customer(int Id, string Name, string Email);

public interface ICustomerSearchService
{
    IReadOnlyList<Customer> Search(string query, int limit);
}

public sealed class InMemoryCustomerSearchService : ICustomerSearchService
{
    private static readonly Customer[] Customers =
    [
        new(1, "Alice Johnson", "alice.johnson@example.com"),
        new(2, "Bob Smith", "bob.smith@example.com"),
        new(3, "Carol Williams", "carol.williams@example.com")
    ];

    public IReadOnlyList<Customer> Search(string query, int limit)
    {
        var normalizedQuery = query.Trim();

        return Customers
            .Where(customer =>
                customer.Name.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ||
                customer.Email.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
            .Take(limit)
            .ToArray();
    }
}
