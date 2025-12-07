using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace tp6.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class JWTBearerTokenSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireTimeInSeconds { get; set; }
    }

    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
