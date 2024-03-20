namespace BLL.Dto.Product
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool? IsActive { get; set; }
    }
}
