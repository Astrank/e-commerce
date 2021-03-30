using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EPE.Domain.Models;
using System.Linq;

namespace EPE.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStock> OrderStocks { get; set; }
        public DbSet<StockOnHold> StockOnHold { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderStock>()
                .HasKey(x => new { x.StockId, x.OrderId });

            builder.Entity<Product>()
                .Property(p => p.Value)
                .HasColumnType("decimal(10,2)");
            
            builder.Entity<Category>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Subcategories)
                .HasForeignKey(x => x.ParentId);
        }
    }
}

/*
WITH ret AS
(
    SELECT * FROM dbo.Categories
    WHERE Id = 6
    UNION ALL
    SELECT t.* FROM dbo.Categories t 
    INNER JOIN ret r ON r.ParentId = t.Id
),
ter AS
(
	SELECT * FROM dbo.Categories
    WHERE Id = 6
    UNION ALL
    SELECT t.* FROM dbo.Categories t 
    INNER JOIN ter r ON r.Id = t.ParentId
)

SELECT * FROM ret
UNION SELECT * FROM ter;
*/