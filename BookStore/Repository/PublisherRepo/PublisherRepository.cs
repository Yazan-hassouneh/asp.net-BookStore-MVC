using BookStore.Data;
using BookStore.Repository.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.PublisherRepo
{
	public class PublisherRepository(ApplicationDbContext context) : BaseRepository<Publisher>(context), IPublisherRepository
	{
		private readonly ApplicationDbContext _context = context;
		public async Task<bool> CheckName(string name)
		{
			var isExist = await _context.Categories.AnyAsync(x => x.Name == name);
			return isExist;
		}
	}
}
