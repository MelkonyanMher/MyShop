using Microsoft.EntityFrameworkCore;
using Tionit.MyShop.Persistence.EntityConfigs;

namespace Tionit.MyShop.Persistence
{
    public class AppDbContext : DbContext
    {
        #region Constructor
        
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        #endregion Constructor

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AdministratorConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new OrderItemConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
    }
}
