using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public sealed class InMemoryProductRepository : IProductRepository
{
    private static readonly Product[] Products =
    [
        new(1, "Laptop", 999.99m),
        new(2, "Wireless Mouse", 29.99m),
        new(3, "Mechanical Keyboard", 79.99m)
    ];

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<Product>>(Products);
    }
}
