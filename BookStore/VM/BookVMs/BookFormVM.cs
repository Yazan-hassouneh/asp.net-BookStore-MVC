using BookStore.CustomAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.VM.BookVMs
{
	public class BookFormVM : BaseVM
	{
		[AllowedExtension(FileSettings.ImagesAllowedExtension)]
		[FileMaxSize(FileSettings.ImagesMaxSizeInBytes)]
		public IFormFile? Image { get; set; }
		public int Price { get; set; }
		public short ReleaseYear { get; set; }
		public int PublisherId { get; set; }
		public IEnumerable<SelectListItem> PublishersList { get; set; } = [];
		public List<int> AuthorsId { get; set; } = [];
		public IEnumerable<SelectListItem> AuthorsList { get; set; } = [];
		public List<int> CategoriesId { get; set; } = [];
		public IEnumerable<SelectListItem> CategoriesList { get; set; } = [];
	}
}
