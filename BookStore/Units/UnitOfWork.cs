using BookStore.Data;
using BookStore.Models;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.CategoryRepo;
using BookStore.Repository.PublisherRepo;

namespace BookStore.Units
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Book> Books { get; private set; }
        public IBaseRepository<Author> Authors { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPublisherRepository Publishers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BaseRepository<Book>(_context);
            Authors = new BaseRepository<Author>(_context);
            Categories = new CategoryRepository(_context);
            Publishers = new PublisherRepository(_context);
        }
        public async Task<int> Complete() => await _context.SaveChangesAsync();
        

        public void Dispose()
        {
            _context.Dispose(); 
        }
    }
}
