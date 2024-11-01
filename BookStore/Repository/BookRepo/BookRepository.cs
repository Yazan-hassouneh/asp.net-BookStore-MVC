using BookStore.Data;
using BookStore.Repository.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.BookRepo
{
	public class BookRepository(ApplicationDbContext context) : BaseRepository<Book>(context), IBookRepository 
	{
		private readonly ApplicationDbContext _context = context;

		public override async Task<Book?> GetByIdAsync(int id)
		{
			return await _context.Books
				.Include(book => book.Categories)
				.ThenInclude(category => category.Category)
				.Include(book => book.Authors)
				.ThenInclude(author => author.Author)
				.Include(book => book.Publisher)
				.AsNoTracking()
				.FirstOrDefaultAsync(book => book.Id == id);
		}
		public override async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _context.Books
				.Include(book => book.Categories)
				.ThenInclude(category => category.Category)
				.Include(book => book.Authors)
				.ThenInclude(author => author.Author)
				.Include(book => book.Publisher)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
