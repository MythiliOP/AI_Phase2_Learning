using Week1_Project_WithCopilot.Models;

namespace Week1_Project_WithCopilot.Services;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
}
