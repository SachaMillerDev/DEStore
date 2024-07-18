using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerformanceReportingService.Data;

namespace PerformanceReportingService
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
            services.AddDbContext<PerformanceReportContext>(options =>
                options.UseInMemoryDatabase("PerformanceReportDb"));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PerformanceReportContext context)
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

        private static void AddTestData(PerformanceReportContext context)
        {
            if (!context.PerformanceReports.Any())
            {
                context.PerformanceReports.AddRange(
                    new PerformanceReport { Id = 1, StoreName = "Store A", TotalSales = 100, TotalTransactions = 50, Revenue = 10000, Expenses = 8000, NetProfit = 2000 },
                    new PerformanceReport { Id = 2, StoreName = "Store B", TotalSales = 200, TotalTransactions = 150, Revenue = 20000, Expenses = 16000, NetProfit = 4000 }
                );
                context.SaveChanges();
            }
        }
    }
}
