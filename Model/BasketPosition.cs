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
    public class BasketPosition : IEntityTypeConfiguration<BasketPosition>
    {
        [Key, Column("ID")]
        public int BasketPositionId { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public required Product Product { get; set; }



        [Column("UserID")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public required User User { get; set; }
        public int Amount { get; set; }

        public void Configure(EntityTypeBuilder<BasketPosition> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.BasketPositions)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Product)
                .WithMany(x => x.BasketPositions)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
