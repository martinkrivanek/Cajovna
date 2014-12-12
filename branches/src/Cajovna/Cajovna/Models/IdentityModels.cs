using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cajovna.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    /* Application DB context is a class which defines what sets (database tables) will
     * be at programmer disposal*/
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Surovina> Suroviny { get; set; }
        public DbSet<Stul> Stoly { get; set; }
        public DbSet<Ucet> Ucty { get; set; }
        public DbSet<Slozeni> Slozeni { get; set; }
        public DbSet<PolozkaMenu> PolozkyMenu { get; set; }
        public DbSet<PolozkaUctu> PolozkyUctu { get; set; }

        /* overrides the default empty method. When creating model structure we sometime 
         * must define some mapping of models - usualy with many-2-one/many associations */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stul>().HasMany(a => a.ucty).WithRequired(b => b.stul).HasForeignKey(c => c.stulID);
            modelBuilder.Entity<Ucet>().HasMany(a => a.polozkyUctu).WithRequired(b => b.ucet).HasForeignKey(c => c.ucetID);
            modelBuilder.Entity<PolozkaMenu>().HasMany(a => a.recipe).WithRequired(b => b.polozkaMenu).HasForeignKey(c => c.polozkaMenuID);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}