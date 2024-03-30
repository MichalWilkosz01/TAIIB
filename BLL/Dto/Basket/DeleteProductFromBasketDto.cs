using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Basket
{
    public class DeleteProductFromBasketDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
