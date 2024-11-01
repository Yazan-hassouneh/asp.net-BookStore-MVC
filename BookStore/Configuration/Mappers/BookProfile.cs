using BookStore.VM.BookVMs;

namespace BookStore.Configuration.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookVM>()
                .ForMember(destination => destination.Authors,
                            option => option.MapFrom(src => src.Authors.Select(x => x.Author.Name))
                )
                .ForMember(destination => destination.Categories,
                            option => option.MapFrom(src => src.Categories.Select(x => x.Category.Name))
                )
                .ReverseMap();

            CreateMap<CreateBookVM, Book>()
                .ForMember(destination => destination.PublisherId,
                            option => option.MapFrom(src => src.PublisherId)
                )
                .ForMember(destination => destination.Categories,
                            option => option.MapFrom(src => src.CategoriesId.Select(categoryId => new BookCategory
                            {
                                CategoryId = categoryId,
                            }).ToList())
                )
                .ForMember(destination => destination.Authors,
                            option => option.MapFrom(src => src.AuthorsId.Select(authorId => new BookAuthor
                            {
                                AuthorId = authorId,
                            }).ToList())
				)
				.ReverseMap();       
            
            CreateMap<Book, UpdateBookVM>()
                .ForMember(destination => destination.PublisherId,
                            option => option.MapFrom(src => src.PublisherId)
                )
                .ForMember(destination => destination.CategoriesId,
                            option => option.MapFrom(src => src.Categories.Select(category => category.CategoryId).ToList())
                )
                .ForMember(destination => destination.AuthorsId,
							option => option.MapFrom(src => src.Authors.Select(author => author.AuthorId).ToList())
				)
				.ReverseMap();            
		}
    }
}
