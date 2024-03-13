using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Product 
    {
        [Key, Column("ProductID")]
        public int ProductId { get; set; }

        [MaxLength(30)]
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public required string Image { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<BasketPosition>? BasketPositions { get; set; }


    }
}
