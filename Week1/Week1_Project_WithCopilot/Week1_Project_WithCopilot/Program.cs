using Week1_Project_WithCopilot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICustomerSearchService, InMemoryCustomerSearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/customers/search", (
    string? name,
    int? limit,
    ICustomerSearchService customerSearchService) =>
{
    if (string.IsNullOrWhiteSpace(name))
    {
        return Results.BadRequest(new { error = "The 'name' query parameter is required." });
    }

    if (limit is < 1 or > 100)
    {
        return Results.BadRequest(new { error = "The 'limit' query parameter must be between 1 and 100." });
    }

    var customers = customerSearchService.Search(name, limit ?? 25);
    return Results.Ok(customers);
})
.WithName("SearchCustomers")
.WithSummary("Search customers by name or email.")
.WithDescription("Returns up to the requested number of customers whose name or email contains the query.")
.WithTags("Customers");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
