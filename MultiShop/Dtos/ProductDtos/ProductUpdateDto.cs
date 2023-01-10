namespace MultiShop.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {
        public ProductGetDto ProductGetDto { get; set; }
        public ProductPostDto ProductPostDto { get; set; }
        public ProductUpdateDto()
        {
            ProductPostDto = new ProductPostDto();
            ProductGetDto = new ProductGetDto();
        }
    }
}
