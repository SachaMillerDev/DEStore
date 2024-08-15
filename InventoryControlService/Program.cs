using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using InventoryControlService.Data;
using InventoryControlService.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InventoryControlContext>(options =>
    options.UseInMemoryDatabase("InventoryControlDB"));
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventoryControlService API", Version = "v1" });
});

// Add logging
builder.Services.AddLogging();

// Build the app
var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InventoryControlContext>();
    SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryControlService API v1");
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

void SeedData(InventoryControlContext context)
{
    if (!context.InventoryItems.Any())
    {
        context.InventoryItems.AddRange(
            new InventoryItem { Name = "Item1", Quantity = 100 },
            new InventoryItem { Name = "Item2", Quantity = 200 },
            new InventoryItem { Name = "Item3", Quantity = 300 }
        );
        context.SaveChanges();
    }
}
