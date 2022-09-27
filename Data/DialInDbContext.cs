using Microsoft.EntityFrameworkCore; 
using DialInApi.Models;

namespace DialInApi.Data
{
    public class DialInDbContext : DbContext
    {
        public DialInDbContext(DbContextOptions<DialInDbContext> options)
            : base(options) {   }

        public DbSet<DialIn> DialIns => Set<DialIn>();
    }
}