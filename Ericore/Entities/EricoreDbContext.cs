using Microsoft.EntityFrameworkCore;

namespace Ericore.Entities
{
    public class EricoreDbContext : DbContext
    {
        public EricoreDbContext(DbContextOptions<EricoreDbContext> options) : base(options) {}
        public DbSet<Comment> Comments { get; set; }
    }
}
