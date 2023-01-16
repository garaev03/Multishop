namespace MultiShop.Dtos.AppUserDtos
{
    public class ProfileUpdateDto
    {
        public ProfileGetDto GetDto { get; set; }
        public ProfilePostDto PostDto { get; set; }
        public ProfileUpdateDto()
        {
            GetDto = new();
            PostDto = new();
        }
    }
}
