using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PriceControlService.Data;
using PriceControlService.Models;

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
