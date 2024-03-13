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
    public class User 
    {
        [Key, Column("ID")]
        public int UserId { get; set; }

        [MaxLength(30)] 
        public required string Login { get; set; }
        public required string Password { get; set; }
        public Type Type { get; set; }
        public bool IsActive { get; set; }

        //Relacja z Order jeden User do wielu Order
        public IEnumerable<Order>? Orders { get; set; }

        public IEnumerable<BasketPosition>? BasketPositions { get; set; }

    }
}
