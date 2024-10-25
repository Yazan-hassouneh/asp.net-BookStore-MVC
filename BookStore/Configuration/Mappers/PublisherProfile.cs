using BookStore.VM.PublisherVMs;

namespace BookStore.Configuration.Mappers
{
	public class PublisherProfile : Profile
	{
        public PublisherProfile()
        {
			CreateMap<Publisher, PublisherVM>()
					.ForMember(destination => destination.NumberOfBooks,
								option => option.MapFrom(src => src.Books.Count)
					)
					.ReverseMap();
			CreateMap<Publisher, CreatePublisherVM>().ReverseMap();
			CreateMap<UpdatePublisherVM, Publisher>()
				.ForMember(destination => destination.CreatedOn,
							option => option.Ignore()
				)
				.ReverseMap();
		}
    }
}
