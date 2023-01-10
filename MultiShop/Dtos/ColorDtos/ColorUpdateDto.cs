namespace MultiShop.Dtos.ColorDtos
{
    public class ColorUpdateDto
    {
        public ColorGetDto colorGetDto { get; set; }
        public ColorPostDto colorPostDto { get; set; }
        public ColorUpdateDto()
        {
            colorGetDto= new ColorGetDto();
            colorPostDto= new ColorPostDto();
        }
    }
}
