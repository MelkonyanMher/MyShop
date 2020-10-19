﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Persistence.EntityConfigs
{
    class AdministratorConfig : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(i => i.Id);
        }
    }
}
