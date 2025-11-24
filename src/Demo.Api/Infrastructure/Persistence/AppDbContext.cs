using Demo.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
