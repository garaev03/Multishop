namespace MultiShop.Dtos.SizeDtos
{
    public class SizeUpdateDto
    {
        public SizeGetDto sizeGetDto { get; set; }
        public SizePostDto sizePostDto { get; set; }
        public SizeUpdateDto()
        {
            sizeGetDto=new SizeGetDto();
            sizePostDto=new SizePostDto();
        }
    }
}
