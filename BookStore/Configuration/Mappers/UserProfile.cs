namespace BookStore.Configuration.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserVM>().ReverseMap();
            CreateMap<ApplicationUserFormVM, ApplicationUser>().ReverseMap();
                
                ;
        }
    }
}
