using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace tp5.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<PanierParUser> Paniers { get; set; }
    }

    public class Produit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class PanierParUser
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public Guid ProduitId { get; set; }
        public List<Produit> produits { get; set; }
    }
}
