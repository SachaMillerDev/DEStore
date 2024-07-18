using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LoyaltyCardService.Data;
using LoyaltyCardService.Models;

namespace LoyaltyCardService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Ensure you have the using directive for Entity Framework Core at the top
            services.AddDbContext<LoyaltyCardContext>(options =>
                options.UseInMemoryDatabase("LoyaltyCardDb"));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LoyaltyCardContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ensure the database is created and seed data is added
            context.Database.EnsureCreated();
            AddTestData(context);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddTestData(LoyaltyCardContext context)
        {
            if (!context.LoyaltyCards.Any())
            {
                context.LoyaltyCards.AddRange(
                    new LoyaltyCard { Id = 1, CustomerName = "John Doe", CardNumber = "1234567890", Points = 100 },
                    new LoyaltyCard { Id = 2, CustomerName = "Jane Smith", CardNumber = "0987654321", Points = 200 }
                );
                context.SaveChanges();
            }
        }
    }
}
