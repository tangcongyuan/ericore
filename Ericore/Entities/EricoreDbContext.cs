using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ericore.Entities
{
    public class EricoreDbContext : IdentityDbContext<User>
    {
        public EricoreDbContext(DbContextOptions<EricoreDbContext> options) : base(options) {}
        public DbSet<Comment> Comments { get; set; }
    }
}
