using BookStore.Data;
using BookStore.Models;
using BookStore.Repository.BaseRepository;

namespace BookStore.Units
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Book> Books { get; private set; }
        public IBaseRepository<Author> Authors { get; private set; }
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<Publisher> Publishers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BaseRepository<Book>(_context);
            Authors = new BaseRepository<Author>(_context);
            Categories = new BaseRepository<Category>(_context);
            Publishers = new BaseRepository<Publisher>(_context);
        }
        public async Task<int> Complete() => await _context.SaveChangesAsync();
        

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
