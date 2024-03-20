using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderPosition : IEntityTypeConfiguration<OrderPosition>
    {
        [Key, Column("ID")]
        public int OrderPositionId { get; set; }


        [Column("OrderID")]
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public required Order Order { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public required Product Product { get; set; }

        public void Configure(EntityTypeBuilder<OrderPosition> builder)
        {
            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderPositions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderPositions)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
