﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Persistence.EntityConfigs
{
    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.Orders)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(MAX) COLLATE Cyrillic_General_CS_AS");
        }
    }
}
