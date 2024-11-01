using BookStore.Data;
using BookStore.Repository.BaseRepo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.PublisherRepo
{
	public class PublisherRepository(ApplicationDbContext context) : BaseRepository<Publisher>(context), IPublisherRepository
	{
		private readonly ApplicationDbContext _context = context;
		public async Task<bool> CheckName(string name)
		{
			var isExist = await _context.Publishers.AnyAsync(x => x.Name == name);
			return isExist;
		}

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
			return [.._context.Publishers.Select(publisher => new SelectListItem {
				Value = publisher.Id.ToString(),
				Text = publisher.Name
			}).OrderBy(x => x.Text).AsNoTracking()];
        }
    }
}
