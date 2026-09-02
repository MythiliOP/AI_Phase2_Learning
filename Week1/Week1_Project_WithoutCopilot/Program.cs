using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Swagger/OpenAPI (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var customers = new[]
{
    new Customer(1, "Alice Johnson", "alice@example.com", "555-0100"),
    new Customer(2, "Bob Smith", "bob@example.com", "555-0111"),
    new Customer(3, "Charlie Lee", "charlie@example.com", "555-0122"),
    new Customer(4, "Diana Ross", "diana.ross@example.com", "555-0133")
};

app.MapGet("/customers", (string? q) =>
{
    if (string.IsNullOrWhiteSpace(q))
        return Results.Ok(customers);

    var normalized = q.Trim().ToLowerInvariant();
    var results = customers.Where(c =>
        (c.Name != null && c.Name.ToLowerInvariant().Contains(normalized)) ||
        (c.Email != null && c.Email.ToLowerInvariant().Contains(normalized)) ||
        (c.Phone != null && c.Phone.ToLowerInvariant().Contains(normalized))
    ).ToArray();

    return Results.Ok(results);
})
.WithName("SearchCustomers")
.Produces<IEnumerable<Customer>>(StatusCodes.Status200OK);


app.Run();

