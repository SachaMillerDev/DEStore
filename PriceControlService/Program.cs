using Microsoft.EntityFrameworkCore;
using PriceControlService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PriceControlContext>(options =>
    options.UseInMemoryDatabase("PriceControlDB"));
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
