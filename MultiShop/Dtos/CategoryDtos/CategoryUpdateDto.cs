namespace MultiShop.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public CategoryGetDto categoryGetDto { get; set; }
        public CategoryPostDto categoryPostDto { get; set; }

        public CategoryUpdateDto()
        {
            categoryGetDto= new CategoryGetDto();
            categoryPostDto= new CategoryPostDto();
        }
    }
}
