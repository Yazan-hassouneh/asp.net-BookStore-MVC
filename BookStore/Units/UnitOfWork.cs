using BookStore.Data;
using BookStore.Repository.AuthorRepo;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.BookRepo;
using BookStore.Repository.CategoryRepo;
using BookStore.Repository.PublisherRepo;

namespace BookStore.Units
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPublisherRepository Publishers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BookRepository(_context);
            Authors = new AuthorRepository(_context);
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
