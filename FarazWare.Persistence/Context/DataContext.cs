using FarazWare.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarazWare.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TokenResponse> UserTokens { get; set; }

    }
}
