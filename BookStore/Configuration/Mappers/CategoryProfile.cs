using AutoMapper;
using BookStore.Models;
using BookStore.VM.CategoryVMs;

namespace BookStore.Configuration.Mappers
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile() 
		{
			CreateMap<Category, CategoryVM>()
				.ForMember(destination => destination.NumberOfBooksInCategory,
							option => option.MapFrom(src => src.Books.Count)
				)
				.ReverseMap();
			CreateMap<Category, CreateCategoryVM>().ReverseMap();
				
		}

	}
}
