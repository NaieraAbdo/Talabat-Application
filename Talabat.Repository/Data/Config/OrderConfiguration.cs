using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)
                .HasConversion(OStat =>OStat.ToString(),OStat =>(OrderStatus)Enum.Parse(typeof(OrderStatus),OStat));
            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.ShippingAddress,
                SA => SA.WithOwner());
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
