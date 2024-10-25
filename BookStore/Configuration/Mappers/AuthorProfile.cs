namespace BookStore.Configuration.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorVM>()
                .ForMember(destination => destination.NumberOfBooks,
                            option => option.MapFrom(src => src.Books.Count)
                )
                .ReverseMap();            
            CreateMap<Author, CreateAuthorVM>().ReverseMap();
			CreateMap<UpdateAuthorVM, Author>()
	            .ForMember(destination => destination.CreatedOn,
				            option => option.Ignore()
	            )
	            .ReverseMap();
		}
    }
}
