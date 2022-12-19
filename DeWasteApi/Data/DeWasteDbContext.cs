using Microsoft.EntityFrameworkCore;
using DeWasteApi.Models;


namespace DeWasteApi.Data
{
    public class DeWasteDbContext : DbContext
    {
        public DeWasteDbContext(DbContextOptions<DeWasteDbContext> options) : base(options)
        {
            
            
        }

        


        public DbSet<Category> categories { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Item_Category> items_categories { get; set; }
        public DbSet<Comment> comments { get; set; } = default!;
        public DbSet<Rating> ratings { get; set; }
    }
}
