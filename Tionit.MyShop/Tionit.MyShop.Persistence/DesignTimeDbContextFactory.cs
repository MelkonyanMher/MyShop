using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tionit.ShopOnline.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            string connectionString =
                "Data Source=192.168.77.18;Initial Catalog=ShopOnline;Persist Security Info=True;User ID=dev;password=sqldevelopment#082015#;";

            builder.UseSqlServer(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
