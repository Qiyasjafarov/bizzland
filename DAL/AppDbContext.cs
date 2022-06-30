using BizLand.Models;
using Microsoft.EntityFrameworkCore;

namespace BizLand.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<PortfolioCategory> Categories { get; set; }
        public DbSet<PortfolioItem> Portfolios { get; set; }
    }
}
