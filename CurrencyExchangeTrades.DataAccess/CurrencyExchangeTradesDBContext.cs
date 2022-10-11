using CurrencyExchangeTrades.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CurrencyExchangeTrades.DataAccess
{
    public class CurrencyExchangeTradesDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public CurrencyExchangeTradesDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Trade>()
               .HasOne(p => p.From)
               .WithMany()
               .HasForeignKey(m => m.FromId)
               .OnDelete(DeleteBehavior.NoAction);

            _ = modelBuilder.Entity<Trade>()
               .HasOne(p => p.To)
               .WithMany()
               .HasForeignKey(m => m.ToId)
               .OnDelete(DeleteBehavior.NoAction);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            _ = optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }
        public DbSet<CurrencySymbol> CurrencySymbols { get; set; }
        public DbSet<Trade> Trades { get; set; }
    }
}