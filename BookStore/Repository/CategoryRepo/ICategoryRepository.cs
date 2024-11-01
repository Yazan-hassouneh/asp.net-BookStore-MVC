using BookStore.Repository.BaseRepo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Repository.CategoryRepo
{
	public interface ICategoryRepository : IBaseRepository<Category>
	{
		Task<bool> CheckName(string name);
		IEnumerable<SelectListItem> GetSelectListItems();
	}
}
