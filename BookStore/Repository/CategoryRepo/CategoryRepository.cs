using BookStore.Data;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.CategoryRepo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.CategoryRepo
{
	public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<bool> CheckName(string name)
		{
			var isExist = await _context.Categories.AnyAsync(x => x.Name == name);
			return isExist;
		}

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
			return [.._context.Categories.Select(category => new SelectListItem {
				Value = category.Id.ToString(),
				Text = category.Name
			}).OrderBy(x => x.Text).AsNoTracking()];
        }
    }
}
