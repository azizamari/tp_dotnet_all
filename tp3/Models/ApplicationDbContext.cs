using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using tp3.Interceptors;

namespace tp3.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            try 
            {
                if (System.IO.File.Exists("Movies.json"))
                {
                    string MovJson = System.IO.File.ReadAllText("Movies.json");
                    List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(MovJson);
                    
                    if (movies != null)
                    {
                        foreach (Movie c in movies)
                        {
                            modelBuilder.Entity<Movie>().HasData(c);
                        }
                    }
                }
            }
            catch(Exception) {}
        }
    }
}
