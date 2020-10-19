﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Persistence.EntityConfigs
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                /*.IsRequired()*/;
        }
    }
}
