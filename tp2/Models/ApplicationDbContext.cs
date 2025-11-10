using Microsoft.EntityFrameworkCore;

namespace tp2.Models
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext> options) : base(options)
        {
        }

        public DbSet<Movie> movies { get; set; }
        public DbSet<Genre> genres { get; set; }
    }
}
