using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
}
