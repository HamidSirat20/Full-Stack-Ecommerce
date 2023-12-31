using backend.Domain.src.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace backend.WebApi.src.Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }

    static DatabaseContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new NpgsqlDataSourceBuilder(
            _configuration.GetConnectionString("DefaultConnection")
        );
        builder.MapEnum<Role>();
        builder.MapEnum<OrderStatus>();
        optionsBuilder.AddInterceptors(new TimestampInterceptor());
        optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Role>();
        modelBuilder.HasPostgresEnum<OrderStatus>();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Category>().HasIndex(u => u.CategoryName).IsUnique();
        modelBuilder.Entity<Category>().Property(u => u.CategoryName).IsRequired();
        base.OnModelCreating(modelBuilder);
    }
}
