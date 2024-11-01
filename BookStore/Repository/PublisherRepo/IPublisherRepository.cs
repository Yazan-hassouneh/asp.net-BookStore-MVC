using BookStore.Repository.BaseRepo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Repository.PublisherRepo
{
	public interface IPublisherRepository : IBaseRepository<Publisher>
	{
		Task<bool> CheckName(string name);
		IEnumerable<SelectListItem> GetSelectListItems();
	}
}
