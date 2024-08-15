using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PriceControlService.Data;
using PriceControlService.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PriceControlContext>(options =>
    options.UseInMemoryDatabase("PriceControlDB"));
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PriceControlService API", Version = "v1" });
});

// Add logging
builder.Services.AddLogging();

// Build the app
var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PriceControlContext>();
    SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PriceControlService API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the root of the application
    });
}

// Custom error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An unhandled exception occurred.");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedData(PriceControlContext context)
{
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Product1", Price = 10.0m, SaleType = "None" },
            new Product { Name = "Product2", Price = 20.0m, SaleType = "Buy one get one free" },
            new Product { Name = "Product3", Price = 30.0m, SaleType = "3 for 2" }
        );
        context.SaveChanges();
    }
}
