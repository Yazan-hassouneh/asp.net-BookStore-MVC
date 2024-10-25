using BookStore.Data;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.CategoryRepo;
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
	}
}
