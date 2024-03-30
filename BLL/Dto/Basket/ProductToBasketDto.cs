using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Basket
{
    public class ProductToBasketDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
    }
}
