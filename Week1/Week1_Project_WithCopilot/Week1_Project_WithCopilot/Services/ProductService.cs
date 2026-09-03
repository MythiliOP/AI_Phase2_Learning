using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Repositories;

namespace Week1_Project_WithCopilot.Services;

public sealed class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken) =>
        productRepository.GetAllAsync(cancellationToken);
}
