using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionItem> TransactionItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration()).Add(new IdentityUserRoleConfiguration());
        }

        public System.Data.Entity.DbSet<Models.Transaction.TransactionListItem> TransactionListItems { get; set; }

        public System.Data.Entity.DbSet<Models.Transaction.TransactionCreate> TransactionCreates { get; set; }

        public System.Data.Entity.DbSet<Models.Transaction.TransactionDetail> TransactionDetails { get; set; }

        public System.Data.Entity.DbSet<Models.Transaction.TransactionEdit> TransactionEdits { get; set; }

        public System.Data.Entity.DbSet<Models.Transaction.TransactionDelete> TransactionDeletes { get; set; }

        public System.Data.Entity.DbSet<Models.Store.StoreListItem> StoreListItems { get; set; }

        public System.Data.Entity.DbSet<Models.Store.StoreCreate> StoreCreates { get; set; }

        public System.Data.Entity.DbSet<Models.Store.StoreDetail> StoreDetails { get; set; }

        public System.Data.Entity.DbSet<Models.Store.StoreEdit> StoreEdits { get; set; }

        public System.Data.Entity.DbSet<Models.Store.StoreDelete> StoreDeletes { get; set; }

        public System.Data.Entity.DbSet<Models.Item.ItemListItem> ItemListItems { get; set; }

        public System.Data.Entity.DbSet<Models.Item.ItemCreate> ItemCreates { get; set; }

        public System.Data.Entity.DbSet<Models.Item.ItemDetail> ItemDetails { get; set; }

        public System.Data.Entity.DbSet<Models.Item.ItemEdit> ItemEdits { get; set; }

        public System.Data.Entity.DbSet<Models.Item.ItemDelete> ItemDeletes { get; set; }
    }

    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }

    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }
}