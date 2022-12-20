using Microsoft.EntityFrameworkCore;
using DeWasteApi.Models;
using DeWasteApi.Interceptors;

namespace DeWasteApi.Data
{
    public class DeWasteDbContext : DbContext
    {
        public DeWasteDbContext(DbContextOptions<DeWasteDbContext> options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.AddInterceptors(new TimeLogInterceptor());

            optionsBuilder.UseNpgsql("Server=database-1.cmqb7wvgno7f.eu-west-2.rds.amazonaws.com;Port=5432;Database=postgres;User Id=postgres;Password=dewaste12;");

        }




        public DbSet<Category> categories { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Item_Category> items_categories { get; set; }
        public DbSet<Comment> comments { get; set; } = default!;
        public DbSet<Rating> ratings { get; set; }
    }

    public class MyLoggerFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public MyLoggerFactory()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("DeWaste", LogLevel.Debug)
                    .AddConsole();
            });
        }

        public ILoggerFactory GetLoggerFactory()
        {
            return _loggerFactory;
        }
    }
}
