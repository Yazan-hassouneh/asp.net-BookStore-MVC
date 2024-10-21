

namespace BookStore.VM.CategoryVMs
{
	public class UpdateCategoryVM : BaseAddImageVM
	{
		public string ImagePath { get; set; } = null!;
		public DateTime UpdatedOn { get; set; } = DateTime.Now;
	}
}
