using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tionit.MyShop.Domain;

namespace Tionit.MyShop.Persistence.EntityConfigs
{
    class AdministratorConfig : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(i => i.Id);
        }
    }
}
