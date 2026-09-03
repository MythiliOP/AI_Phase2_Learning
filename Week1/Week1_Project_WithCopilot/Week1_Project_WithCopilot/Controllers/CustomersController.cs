using Microsoft.AspNetCore.Mvc;
using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Services;

namespace Week1_Project_WithCopilot.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController(ICustomerSearchService customerSearchService) : ControllerBase
{
    [HttpGet("search")]
    [ProducesResponseType<IReadOnlyList<Customer>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<Customer>>> Search(
        [FromQuery] string? name,
        [FromQuery] int? limit,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new { error = "The 'name' query parameter is required." });
        }

        if (limit is < 1 or > 100)
        {
            return BadRequest(new { error = "The 'limit' query parameter must be between 1 and 100." });
        }

        var customers = await customerSearchService.SearchAsync(
            name,
            limit ?? 25,
            cancellationToken);

        return Ok(customers);
    }
}
