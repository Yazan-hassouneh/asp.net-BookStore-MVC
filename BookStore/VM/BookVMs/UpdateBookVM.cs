using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.VM.BookVMs
{
	public class UpdateBookVM : BookFormVM
	{
		public string ImagePath { get; set; } = null!;
		public DateTime UpdatedOn { get; set; } = DateTime.Now;
	}
}
