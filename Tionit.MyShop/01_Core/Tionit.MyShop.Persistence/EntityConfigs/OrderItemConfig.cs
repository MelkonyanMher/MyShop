﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tionit.ShopOnline.Domain;

namespace Tionit.ShopOnline.Persistence.EntityConfigs
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.Order)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
